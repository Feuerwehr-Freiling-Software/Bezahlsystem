﻿using OAOPS.Client.DTO;
using OAOPS.Client.ViewModels;

namespace OAOPS.Client.Services
{
    public interface IDataService
    {
        #region Suggestions

        public Task<List<ErrorDto>?> AddSuggestion(SuggestionDTO suggestion);
        public Task<List<SuggestionDTO>?> GetAllSuggestions();

        #endregion

        #region Errors
        
        public Task<List<ErrorDto>> GetAllErrors();
        public Task<List<ErrorDto>> AddError(ErrorDto error);
        #endregion

        #region Products

        public Task<ErrorDto?> Pay(List<ArticleDto> articles);
        public Task<List<ArticleDto>?> GetArticles();
        public Task<List<ArticleDto>?> GetArticlesFiltered(string? articleName = null, int? page = null, int? pageSize = null);

        #endregion

        #region Storages

        public Task<List<StorageDto>?> GetAllStorages();
        public Task<List<ErrorDto?>> AddStorage(StorageVM storageVM);
        public Task<StorageDto?> GetStorageById(int id);

        #endregion

        #region Slots

        public Task<List<StorageSlotDto>?> GetSlotsOfStorageByName(string name);
        public Task<ErrorDto> UpdateStorageSlot(StorageSlotDto slot);
        public Task<List<ArticleCategoryDto>?> GetCategories();
        public Task<ErrorDto> UpdateCategory(ArticleCategoryDto category);
        public Task<ErrorDto>? DeleteCategory(ArticleCategoryDto category);
        public Task<ErrorDto>? AddArticle(ArticleDto newArticle);
        Task<List<ArticleCategoryDto>?> GetAllCategories();

        #endregion

        #region Users

        public Task<string> GetBalance(string username);

        #endregion
    }
}
