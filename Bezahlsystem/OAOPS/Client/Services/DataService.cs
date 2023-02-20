using Microsoft.Extensions.Options;
using OAOPS.Client.Configuration;
using static System.Net.WebRequestMethods;

namespace OAOPS.Client.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _http;
        private readonly ClientOptions configuration;

        public DataService(HttpClient http, IOptions<ClientOptions> options)
        {
            _http = http;
            configuration = options.Value;
        }

        #region Suggestions

        public async Task<HttpResponseMessage> AddSuggestion(SuggestionDTO suggestion)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddSuggestion, suggestion);
            return res;
        }

        public async Task<List<SuggestionDTO>?> GetAllSuggestions()
        {
            var res = await _http.GetFromJsonAsync<List<SuggestionDTO>>(configuration.ApiEndpoints.GetAllSuggestions);
            return res;
        }

        #endregion

        #region Error



        #endregion

        #region Articles

        public async Task<List<ArticleDto>?> GetArticles()
        {
            var res = await _http.GetFromJsonAsync<List<ArticleDto>>(configuration.ApiEndpoints.GetAllArticles);
            return res;
        }

        public async Task<List<ErrorDto>> Pay(List<ArticleDto> articles, string username)
        {
            var purchase = new PaymentDto() { Articles = articles, Username = username};
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.Pay, purchase);
            var result = await res.Content.ReadFromJsonAsync<List<ErrorDto>>();
            return result;
        }

        #endregion
    }
}
