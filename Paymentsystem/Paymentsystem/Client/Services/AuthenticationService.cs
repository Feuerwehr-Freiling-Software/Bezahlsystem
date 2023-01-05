using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;

namespace Paymentsystem.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _client;

        public AuthenticationService(ILocalStorageService localStorage, HttpClient client)
        {
            _localStorage = localStorage;
            _client = client;
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

            var bodyContent = new StringContent("", Encoding.UTF8, "application/json");

            var refreshResult = await _client.PostAsync("token/refresh", bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            if (!refreshResult.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong during the refresh token action");

            await _localStorage.SetItemAsync("authToken", refreshContent);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", refreshContent);
            return refreshContent;
        }
    }
}
