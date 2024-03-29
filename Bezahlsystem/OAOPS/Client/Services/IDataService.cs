﻿using OAOPS.Client.DTO;
using OAOPS.Client.ViewModels;

namespace OAOPS.Client.Services
{
    public interface IDataService
    {
        #region Suggestions

        public Task<ErrorDto> AddSuggestion(SuggestionDTO suggestion);
        public Task<List<SuggestionDTO>?> GetAllSuggestions();
        public Task<ErrorDto> UpdateSuggestion(SuggestionDTO item);
        public Task<ErrorDto?> DeleteSuggestion(int itemId); 

        #endregion

        #region Errors
        
        public Task<List<ErrorDto>> GetAllErrors();
        public Task<List<ErrorDto>> AddError(ErrorDto error);
        #endregion

        #region Products

        public Task<ErrorDto?> Pay(List<ArticleDto> articles);
        public Task<List<ArticleDto>?> GetArticles();
        public Task<List<ArticleDto>?> GetArticlesFiltered(string? articleName = null, int? page = null, int? pageSize = null);
        public Task<ErrorDto> UpdateArticle(ArticleDto article);
        public Task<ErrorDto?> DeleteArticle(int id);

        #endregion

        #region Storages

        public Task<List<StorageDto>?> GetAllStorages();
        public Task<List<ErrorDto?>> AddStorage(StorageDto storageVM);
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

        public Task<double> GetBalance(string username);
        public Task<List<UserDto>> GetAllUsers();
        public Task<List<UserDto>?> GetUsersFiltered(string? username = null, int? page = null, int? pageSize = null);
        public Task DeactivateUser(string username);
        public Task<ErrorDto> AddStorageSlot(StorageSlotDto newSlot);
        public Task<ErrorDto> DeleteStorageSlot(int slotId);
        public Task<UserStatsDto> GetUserStats(string username);
        public Task<List<ShortCategoryDto>> GetAllCategoriesShort();
        public Task<List<PaymentDto>> GetAllPaymentsFiltered(DateTime? fromDate = null, DateTime? toDate = null, ShortCategoryDto? category = null, double? minAmount = null, double? maxAmount = null);
        public Task<List<TopUpDto>> GetAllTopUpsFiltered(string username, DateTime? fromDate = null, DateTime? toDate = null, string? executor = null, double? amount = null);
        public Task<List<RoleDto>> GetRoles();
        public Task<ErrorDto> UpdateUser(UserDto user);
        public Task<ErrorDto> AddTopUp(double topUpAmount, string username, string executorName);

        #endregion
    }
}
