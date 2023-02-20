using OAOPS.Client.DTO;

namespace OAOPS.Client.Services
{
    public interface IDataService
    {
        #region Suggestions

        public Task<HttpResponseMessage> AddSuggestion(SuggestionDTO suggestion);
        public Task<List<SuggestionDTO>?> GetAllSuggestions();

        #endregion

        #region Errors

        #endregion

        #region Products

        public Task<List<ErrorDto>> Pay(List<ArticleDto> articles, string username);
        public Task<List<ArticleDto>> GetArticles();

        #endregion
    }
}
