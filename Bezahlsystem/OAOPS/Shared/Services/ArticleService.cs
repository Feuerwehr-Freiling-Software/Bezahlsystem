using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OAOPS.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;

        public ArticleService(ApplicationDbContext db, IPriceService priceService)
        {
            _db = db;
        }

        public async Task<List<ArticleDto>> GetAllArticles()
        {
            return (from res in _db.Articles
                    select new ArticleDto
                    {
                        Name = res.Name,
                    }).ToList();
        }

        public async Task<List<int>> AddArticle(ArticleDto article)
        {
            // add new Entities to db
            Article newArticle = new()
            {
                Name = article.Name
            };

            Price newPrice = new ()
            {
                Article = newArticle,
                Amount = article.PriceAmount,
                Since = DateTime.Now
            };

            ArticleCategory newCategory = new()
            {
                Name = article.Category,
                Parent = null,
                Children = null
            };

            // get all necessary ID's

            var storageSlot = (from storage in _db.Storages
                               join slot in _db.Slots on storage.Id equals slot.StorageId
                               where storage.StorageName == article.StorageName && slot.Name == article.StorageSlot
                               select new StorageSlotDto
                               {
                                   SlotId = slot.Id,
                                   StorageName = storage.StorageName,
                                   SlotName = slot.Name,
                                   StorageConnectionId = storage.ConnectionId,
                                   StorageId = storage.Id
                               }).FirstOrDefault();

            if(storageSlot == null)
            {
                // check if article.Storagename || article.StorageSlot equals null and return corresponding errors
                return new List<int>() { 0 };
            }

            // add Article to storage

            var SlotInStorageHasArticle = new ArticleInStorageSlot
            {
                Article = newArticle,
                MinAmount = article.MinAmount,
                QuantityAtStart = article.QuantityAtStart,
                QuantityActual = article.QuantityAtStart,
                SlotId = storageSlot.SlotId
            };
            return new List<int>() { 0 };
        }
    }
}
