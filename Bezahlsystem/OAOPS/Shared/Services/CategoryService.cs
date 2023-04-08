using Microsoft.EntityFrameworkCore;
using OAOPS.Shared.DTO;
using OAOPS.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(ApplicationDbContext db, IErrorCodeService codeService)
        {
            Db = db;
            CodeService = codeService;
        }

        public ApplicationDbContext Db { get; }
        public IErrorCodeService CodeService { get; }

        public async Task<List<ArticleCategoryDto>> GetCategories()
        {
            var res = from cat in Db.ArticleCategories
                        .Include(c => c.Children)
                        .Include(c => c.Parent)
                      select new ArticleCategoryDto
                      {
                          Children = cat.MapArticleCategoryToDto().Children ?? null,
                          Id = cat.Id,
                          Name = cat.Name,
                          Parent = cat.Parent.MapArticleCategoryToDto() ?? null
                      };
            return await res.ToListAsync();
        }

        public async Task<ErrorDto> AddCategory(ArticleCategoryDto category)
        {
            var fCat = Db.ArticleCategories.FirstOrDefault(x => x.Name == category.Name);
            if (fCat != null)
            {
                return CodeService.GetError(41).MapErrorCodeToDto();
            }

            Db.ArticleCategories.Add(category.MapArticleCategoryDtoToCategory());
            var res = await Db.SaveChangesAsync();

            if (res <= 0)
            {
                return CodeService.GetError(42).MapErrorCodeToDto();
            }

            return CodeService.GetError(40).MapErrorCodeToDto();
        }

        public async Task<ErrorDto> UpdateCategory(ArticleCategoryDto category)
        {
            var fCat = await Db.ArticleCategories.FirstOrDefaultAsync(c => c.Name == category.Name);
            if (fCat == null) return await AddCategory(category);

            fCat.Children = category.MapArticleCategoryDtoToCategory().Children;
            var res = await Db.SaveChangesAsync();

            if (res <= 0)
            {
                return CodeService.GetError(42).MapErrorCodeToDto();
            }

            return CodeService.GetError(40).MapErrorCodeToDto();
        }

        public async Task<ErrorDto> AddArticleToCategory(ArticleDto article, string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName.Trim()))
            {
                return CodeService.GetError(45).MapErrorCodeToDto();
            }

            var fCat = await Db.ArticleCategories.FirstOrDefaultAsync(x => x.Name == categoryName);
            if (fCat == null)
            {
                return CodeService.GetError(41).MapErrorCodeToDto();
            }

            var fArticle = await Db.Articles.FirstOrDefaultAsync(x => x.Name.Equals(article.Name));
            if (fArticle == null)
            {
                return CodeService.GetError(46).MapErrorCodeToDto();
            }

            fCat.Articles.Add(fArticle);

            var res = await Db.SaveChangesAsync();
            if (res <= 0)
            {
                return CodeService.GetError(42).MapErrorCodeToDto();
            }

            return CodeService.GetError(40).MapErrorCodeToDto();
        }

        public async Task<ErrorDto> RemoveArticleFromCategory(ArticleDto article, string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName.Trim()))
            {
                return CodeService.GetError(45).MapErrorCodeToDto();
            }

            var fCat = await Db.ArticleCategories.FirstOrDefaultAsync(x => x.Name == categoryName);
            if (fCat == null)
            {
                return CodeService.GetError(41).MapErrorCodeToDto();
            }

            var fArticle = await Db.Articles.FirstOrDefaultAsync(x => x.Name.Equals(article.Name));
            if (fArticle == null)
            {
                return CodeService.GetError(46).MapErrorCodeToDto();
            }

            fCat.Articles.Remove(fArticle);
            var res = await Db.SaveChangesAsync();
            if (res <= 0)
            {
                return CodeService.GetError(42).MapErrorCodeToDto();
            }
            return CodeService.GetError(40).MapErrorCodeToDto();    
        }
    }
}
