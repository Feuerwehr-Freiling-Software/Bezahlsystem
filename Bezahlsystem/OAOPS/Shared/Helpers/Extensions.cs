using OAOPS.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Helpers
{
    public static class Extensions
    {
        public static ArticleCategoryDto? MapArticleCategoryToDto(this ArticleCategory? category)
        {
            if (category == null) return null;

            var dto = new ArticleCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            if (category.Parent != null)
            {
                dto.Parent = MapArticleCategoryToDto(category.Parent);
            }

           if (category.Children != null)
            {
                foreach (var child in category.Children)
                {
                    dto.Children.Add(MapArticleCategoryToDto(child));
                }
            }

            return dto;
        }

        // method to convert a ArticleCategory object to ArticleCategoryDto


        public static ErrorDto MapErrorCodeToDto(this ErrorCode code)
        {
            return new ErrorDto
            {
                Code = code.Code,
                ErrorText = code.ErrorText,
                IsSuccessCode = code.IsSuccessCode
            };
        }

        // write a method that maps an ArticleCategoryDto to an ArticleCategory
        public static ArticleCategory? MapArticleCategoryDtoToCategory(this ArticleCategoryDto? dto)
        {
            if (dto == null) return null;

            var cat = new ArticleCategory
            {
                Id = dto.Id,
                Name = dto.Name,
                Children = new List<ArticleCategory>()
            };

            if (dto.Parent != null)
            {
                cat.Parent = MapArticleCategoryDtoToCategory(dto.Parent);
            }
            
            if (dto.Children != null)
            {
                foreach (var child in dto.Children)
                {
                    cat?.Children?.Add(MapArticleCategoryDtoToCategory(child));
                }
            }

            return cat;
        }

        // write a method that maps an ErrorDto to an ErrorCode
        public static ErrorCode MapErrorDtoToErrorCode(this ErrorDto dto)
        {
            return new ErrorCode
            {
                Code = dto.Code,
                ErrorText = dto.ErrorText,
                IsSuccessCode = dto.IsSuccessCode
            };
        }


    }
}
