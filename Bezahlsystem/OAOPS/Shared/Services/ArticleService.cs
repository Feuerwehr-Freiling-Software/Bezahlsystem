using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OAOPS.Shared.DTO;
using OAOPS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICategoryService categoryService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IErrorCodeService codeService;

        public ArticleService(ApplicationDbContext db, IPriceService priceService, ICategoryService categoryService, UserManager<ApplicationUser> userManager, IErrorCodeService codeService)
        {
            _db = db;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.codeService = codeService;
        }

        public async Task<List<ArticleDto>> GetAllArticles()
        {
            return await GetAllArticlesFiltered();
        }

        public async Task<List<int>> AddArticle(ArticleDto article)
        {
            // add new Entities to db

            Article newArticle = new()
            {
                Name = article.Name,
                Base64data = article.Base64data
            };

            var category = await categoryService.GetCategoryByName(article.Category);
            if(category != null)
            {
                newArticle.ArticleCategoryId = category.Id;
            }

            var createdArticle = _db.Articles.Add(newArticle);

            Price newPrice = new ()
            {
                Article = newArticle,
                Amount = article.PriceAmount,
                Since = DateTime.Now
            };

            var res1 = _db.Prices.Add(newPrice);
            _db.SaveChanges();
            if (string.IsNullOrEmpty(article.StorageName))
            {
                var res = await _db.SaveChangesAsync();
                switch (res)
                {
                    case < 1:
                        return new List<int>() { 61 };
                    case >= 1:
                        return new List<int>() { 60 };
                }
            }

            // get all necessary ID's

            var storageSlot = (from storage in _db.Storages
                               join slot in _db.Slots on storage.Id equals slot.StorageId
                               where storage.StorageName == article.StorageName && slot.Name == article.StorageSlot
                               select new StorageSlotDto
                               {
                                   SlotId = slot.Id,
                                   StorageName = storage.StorageName,
                                   SlotName = slot.Name,
                                   StorageConnectionId = storage.ConnectionId ?? string.Empty,
                                   StorageId = storage.Id
                               }).FirstOrDefault();

            if(storageSlot == null)
            {
                // check if article.Storagename || article.StorageSlot equals null and return corresponding errors
                return new List<int>() { 0 };
            }

            // add Article to storage

            var fArticle = _db.Articles.FirstOrDefault(x => x.Name == article.Name);
            if(fArticle == null) return new List<int>() { 63 };

            var SlotInStorageHasArticle = new ArticleInStorageSlot
            {
                ArticleId = fArticle.Id,
                MinAmount = article.MinAmount,
                QuantityAtStart = article.QuantityAtStart,
                QuantityActual = article.QuantityAtStart,
                SlotId = storageSlot.SlotId
            };

            _db.ArticleInStorageSlots.Add(SlotInStorageHasArticle);
            var res2 = await _db.SaveChangesAsync();

            switch (res2)
            {
                case < 1:
                    return new List<int>() { 61 };
                case >= 1:
                    return new List<int>() { 60 };
            }
        }

        public async Task<List<ArticleDto>> GetAllArticlesFiltered(string? articleName = null, int? page = null, int? pageSize = null)
        {
            IQueryable<ArticleInStorageSlot> query = _db.ArticleInStorageSlots
                .Include(x => x.Article)
                .ThenInclude(x => x.ArticleCategory)
                .Include(x => x.Slot)
                .ThenInclude(x => x.Storage);

            if (articleName != null)
            {
                query = query.Where(x => x.Article.Name.StartsWith(articleName));
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip(page.Value * pageSize.Value).Take(pageSize.Value);
            }

            var tmp = from res in query
                      select new ArticleDto
                      {
                          MinAmount = res.MinAmount,
                          Amount = 1,
                          PriceAmount = _db.Prices.FirstOrDefault(x => x.ArticleId == res.ArticleId).Amount,
                          QuantityActual = res.QuantityActual,
                          QuantityAtStart = res.QuantityAtStart,
                          Category = res.Article.ArticleCategory.Name,
                          Name = res.Article.Name,
                          StorageName = res.Slot.Storage.StorageName,
                          StorageSlot = res.Slot.Name,
                          Base64data = res.Article.Base64data
                      };

            return await tmp.ToListAsync();
        }

        public async Task<ErrorDto> Pay(List<ArticleDto> articles, string userId)
        {
            // TODO: Payment logic
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null) return new ErrorDto();
            List<UserBoughtArticleFromSlot> payments = new();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ErrorCode, ErrorDto>();
            });

            foreach (var item in articles)
            {
                // get corresponding ArticleInStorageSlot from db

                var storageArticle = _db.ArticleInStorageSlots.Include(x => x.Article).FirstOrDefault(x => x.Article.Name == item.Name);
                if (storageArticle == null) break;
                var payment = new UserBoughtArticleFromSlot()
                {
                    ArticleInStorageSlotId = storageArticle.Id,
                    Quantity = item.Amount,
                    TimeBought = DateTime.Now,
                    UserId = user.Id
                };

                // TODO: Calculate total price and subtract it from User Balance
                var price = _db.Prices.FirstOrDefault(x => x.ArticleId == storageArticle.ArticleId && x.Until == default);
                if (price == null) break;
                
                var totalPrice = price.Amount * item.Amount;
                if (user.Balance < totalPrice) break;

                await _db.UserBoughtArticleFromSlots.AddRangeAsync(payments);
                user.Balance -= totalPrice;
                payments.Add(payment);
            }

            var res = await _db.SaveChangesAsync();

            switch (res)
            {
                case < 1:
                    return new Mapper(mapperConfig).Map<ErrorDto>(codeService.GetError(61));
                case >= 1:
                    return new Mapper(mapperConfig).Map<ErrorDto>(codeService.GetError(60));
            }
        }
    }
}
