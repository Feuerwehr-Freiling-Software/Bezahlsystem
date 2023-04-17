using Microsoft.AspNetCore.Mvc;
using OAOPS.Shared.DTO;
using OAOPS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Interfaces
{
    public interface ICategoryService
    {
        Task<List<ArticleCategoryDto>> GetCategories();
        Task<ErrorDto> AddCategory(ArticleCategoryDto category);
        Task<ErrorDto> UpdateCategory(ArticleCategoryDto category);
        Task<ErrorDto> AddArticleToCategory(ArticleDto article, string categoryName);
        Task<ErrorDto> RemoveArticleFromCategory(ArticleDto article, string categoryName);
        Task<ErrorDto> DeleteCategory(int categoryId);
        Task<List<ArticleCategoryDto>> GetAllCategories();
        Task<ArticleCategory?> GetCategoryByName(string category);
    }
}
