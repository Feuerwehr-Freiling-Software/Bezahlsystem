using Paymentsystem.Shared.Data;
using Paymentsystem.Shared.Interfaces;
using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;

        public ArticleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Article> GetAllArticles()
        {
            return _db.Articles.Include(x => x.Price).ToList();
        }
    }
}
