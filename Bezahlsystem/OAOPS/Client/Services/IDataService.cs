using OAOPS.Client.DTO;
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

        public Task<List<ErrorDto>?> Pay(List<ArticleDto> articles, string username);
        public Task<List<ArticleDto>?> GetArticles();

        #endregion

        #region Storages

        public Task<List<StorageDto>?> GetAllStorages();
        public Task<List<ErrorDto?>> AddStorage(StorageVM storageVM);
        public Task<StorageDto?> GetStorageById(int id);

        #endregion

        #region Slots

        public Task<List<StorageSlotDto>?> GetSlotsOfStorageByName(string name);
        public Task<ErrorDto> UpdateStorageSlot(StorageSlotDto slot);

        #endregion
    }
}
