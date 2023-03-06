using Microsoft.AspNetCore.Mvc;
using OAOPS.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Interfaces
{
    public interface IArticleService
    {
        public Task<List<ArticleDto>> GetAllArticles();
        public Task<List<int>> AddArticle(ArticleDto article);
    }
}
