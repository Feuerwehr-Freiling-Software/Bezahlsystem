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
        public Task<List<ArticleDto>> GetAllArticlesFiltered(string? articleName, int? page, int? pageSize);
        public Task<ErrorDto> Pay(List<ArticleDto> articles, string userId);
        public Task<ErrorDto> UpdateArticle(ArticleDto article);
        public Task<ErrorDto> DeleteArticle(int id);
    }
}
