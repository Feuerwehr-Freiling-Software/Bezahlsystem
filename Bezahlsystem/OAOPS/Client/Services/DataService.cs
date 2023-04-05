using Microsoft.Extensions.Options;
using OAOPS.Client.Configuration;
using OAOPS.Client.DTO;
using OAOPS.Client.ViewModels;
using System.Net.Http.Json;

namespace OAOPS.Client.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _http;
        private readonly ClientOptions configuration;

        [Inject]
        public IAccessTokenProvider TokenProvider { get; set; }

        public DataService(HttpClient http, IOptions<ClientOptions> options)
        {
            _http = http;
            configuration = options.Value;
        }
               

        #region Suggestions

        public async Task<List<ErrorDto>?> AddSuggestion(SuggestionDTO suggestion)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddSuggestion, suggestion);
            var result = await res.Content.ReadFromJsonAsync<List<ErrorDto>?>();
            return result;
        }

        public async Task<List<SuggestionDTO>?> GetAllSuggestions()
        {
            var res = await _http.GetFromJsonAsync<List<SuggestionDTO>?>(configuration.ApiEndpoints.GetAllSuggestions);
            return res;
        }

        #endregion

        #region Error

        public async Task<List<ErrorDto>> AddError(ErrorDto error)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddError, error);
            var result = await res.Content.ReadFromJsonAsync<List<ErrorDto>>();
            return result;
        }

        public async Task<List<ErrorDto>> GetAllErrors()
        {
            var res = await _http.GetFromJsonAsync<List<ErrorDto>>(configuration.ApiEndpoints.GetAllErrors);
            return res;
        }

        #endregion

        #region Articles

        public async Task<List<ArticleDto>?> GetArticles()
        {
            var res = await _http.GetFromJsonAsync<List<ArticleDto>>(configuration.ApiEndpoints.GetAllArticles);
            return res;
        }

        public async Task<List<ErrorDto>?> Pay(List<ArticleDto> articles, string username)
        {
            var purchase = new PaymentDto() { Articles = articles, Username = username};

            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.Pay, purchase);
            var result = await res.Content.ReadFromJsonAsync<List<ErrorDto>?>();
            return result;
        }

        #endregion

        #region Storage

        public async Task<List<StorageDto>?> GetAllStorages()
        {
            var result = await _http.GetAsync(configuration.ApiEndpoints.GetAllStorages);
            if (!result.IsSuccessStatusCode)
            {
                return new();
            }

            return await result.Content.ReadFromJsonAsync<List<StorageDto>>();
        }

        public async Task<List<ErrorDto>?> AddStorage(StorageVM storage)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddStorage, storage);
            var result = await res.Content.ReadFromJsonAsync<List<ErrorDto>?>();
            return result;
        }

        public async Task<StorageDto?> GetStorageById(int id)
        {
            var res = await _http.GetFromJsonAsync<StorageDto?>(configuration.ApiEndpoints.GetStorageById + id);
            return res;
        }

        #endregion

        #region Slots

        public async Task<List<StorageSlotDto>?> GetSlotsOfStorageByName(string name)
        {
            var res = await _http.GetFromJsonAsync<List<StorageSlotDto>?>(configuration.ApiEndpoints.GetSlotsOfStorageByName + name);
            return res;
        }



        #endregion
    }
}
