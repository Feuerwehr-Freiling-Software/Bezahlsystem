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

        public async Task<List<Article>> GetAllArticles()
        {
            return await _db.Articles.ToListAsync();
        }

        public async Task<List<int>> AddArticle(ArticleDto article)
        {
            Article newArticle = new()
            {
                Name = article.Name
            };

            Price price = new Price()
            {
                Article = newArticle,
                Amount = article.Amount,
                Since = DateTime.Now
            };


        }
    }
}
