﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using OAOPS.Client.Configuration;
using OAOPS.Client.DTO;
using OAOPS.Client.ViewModels;
using System.Collections.Generic;
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

        public async Task<ErrorDto> AddSuggestion(SuggestionDTO suggestion)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddSuggestion, suggestion);
            try
            {
                var result = await res.Content.ReadFromJsonAsync<ErrorDto>();
                return result;
            }
            catch (Exception)
            {
                return new ErrorDto { ErrorText = "Fehler beim Hinzufügen des Vorschlags." };
            }
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

        public async Task<ErrorDto?> Pay(List<ArticleDto> articles)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.Pay, articles);
            var result = await res.Content.ReadFromJsonAsync<ErrorDto?>();
            return result;
        }

        public async Task<List<ArticleDto>?> GetArticlesFiltered(string? articleName = null, int? page = null, int? pageSize = null)
        {
            QueryBuilder queryBuilder = new ();
            if(articleName != null)
            {
                queryBuilder.Add(nameof(articleName), articleName);
            }

            if (page != null)
            {
                queryBuilder.Add(nameof(page), page.Value.ToString());
            }

            if(pageSize != null)
            {
                queryBuilder.Add(nameof(pageSize), pageSize.Value.ToString());
            }

            string query = queryBuilder.ToQueryString().ToString();
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetArticlesFiltered + query);
            var result = await res.Content.ReadAsStringAsync();
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<List<ArticleDto>>() : new List<ArticleDto>();
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

        public async Task<List<ErrorDto>?> AddStorage(StorageDto storage)
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
            var result = await _http.GetAsync(configuration.ApiEndpoints.GetSlotsOfStorageByName + name);
            if (!result.IsSuccessStatusCode)
            {
                return new();
            }
            else
            {
                try
                {
                    return await result.Content.ReadFromJsonAsync<List<StorageSlotDto>?>();
                }
                catch (Exception)
                {
                    return new();
                }
            }
        }

        public async Task<ErrorDto> UpdateStorageSlot(StorageSlotDto slot)
        {
            var res = await _http.PutAsJsonAsync(configuration.ApiEndpoints.UpdateStorageSlot, slot);
            return await res.Content.ReadFromJsonAsync<ErrorDto>();
        }

        public async Task<List<ArticleCategoryDto>?> GetCategories()
        {
            var result = await _http.GetAsync(configuration.ApiEndpoints.GetCategories);
            if (!result.IsSuccessStatusCode)
            {
                return new();
            }

            return await result.Content.ReadFromJsonAsync<List<ArticleCategoryDto>>();
        }

        public async Task<ErrorDto?> UpdateCategory(ArticleCategoryDto category)
        {
            var res = await _http.PutAsJsonAsync(configuration.ApiEndpoints.UpdateCategory, category);
            //  check if result is ok and return error 
            if (!res.IsSuccessStatusCode)
            {
                return new ErrorDto() { Code = 1, ErrorText = "Error while adding category", IsSuccessCode = false };
            }

            return await res.Content.ReadFromJsonAsync<ErrorDto>();
        }

        public async Task<ErrorDto>? DeleteCategory(ArticleCategoryDto category)
        {
            var res = await _http.DeleteAsync(configuration.ApiEndpoints.DeleteCategory + "/" + category.Id);
            //  check if result is ok and return error 
            if (!res.IsSuccessStatusCode)
            {
                return new ErrorDto() { Code = 1, ErrorText = "Unexpected error while deleting. See logs for further Information", IsSuccessCode = false };
            }

            return await res.Content.ReadFromJsonAsync<ErrorDto>();
        }

        public async Task<List<ArticleCategoryDto>?> GetAllCategories()
        {
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetAllCategories);
            if (!res.IsSuccessStatusCode)
            {
                return new();
            }

            return await res.Content.ReadFromJsonAsync<List<ArticleCategoryDto>>();
        }

        public async Task<ErrorDto>? AddArticle(ArticleDto newArticle)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddArticle, newArticle);
            if (!res.IsSuccessStatusCode)
            {                
                return new ErrorDto() { Code = 1, ErrorText = "Unexpected error while deleting. See logs for further Information", IsSuccessCode = false };
            }

            return await res.Content.ReadFromJsonAsync<ErrorDto>();
        }

        public async Task<double> GetBalance(string username)
        {
            QueryBuilder builder = new()
            {
                { nameof(username), username }
            };
            var userQuery = builder.ToQueryString().ToString();
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetBalance + userQuery);

            if (!res.IsSuccessStatusCode)
            {
                return 0.0;
            }

            double balance = 0;

            try
            {
                balance = await res.Content.ReadFromJsonAsync<double>();
            }
            catch (Exception)
            {
                await Console.Out.WriteLineAsync(await res.Content.ReadAsStringAsync());
            }
            return balance;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetAllUsers);
            if (!res.IsSuccessStatusCode)
            {
                return new();
            }

            return await res.Content.ReadFromJsonAsync<List<UserDto>>() ?? new();
        }

        public async Task<List<UserDto>?> GetUsersFiltered(string? username = null, int? page = null, int? pageSize = null)
        {
            QueryBuilder builder = new ();
            if (username != null) builder.Add(nameof(username), username);
            if (page != null) builder.Add(nameof(page), page.Value.ToString());
            if (pageSize != null) builder.Add(nameof(pageSize), pageSize.Value.ToString());

            var query = builder.ToQueryString().ToString();
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetUsersFiltered + query);
            if (!res.IsSuccessStatusCode)
            {
                return new();
            }

            return await res.Content.ReadFromJsonAsync<List<UserDto>>();
        }

        public async Task DeactivateUser(string username)
        {
            var res = await _http.DeleteAsync(configuration.ApiEndpoints.DeactivateUser + "?" + nameof(username) + "=" + username);
        }

        public async Task<ErrorDto> UpdateUser(UserDto user)
        {
            var res = await _http.PutAsJsonAsync(configuration.ApiEndpoints.UpdateUser, user);
            if (!res.IsSuccessStatusCode)
            {
                return new ErrorDto() { Code = 1, ErrorText = "Unexpected error while updating User. See logs for further Information", IsSuccessCode = false };
            }
            return await res.Content.ReadFromJsonAsync<ErrorDto>() ?? new ErrorDto();
        }

        public async Task<ErrorDto> AddStorageSlot(StorageSlotDto newSlot)
        {
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddStorageSlot, newSlot);
            if (!res.IsSuccessStatusCode)
            {
                return new ErrorDto() { Code = 1, ErrorText = "Unexpected error while adding Storageslot. See logs for further Information", IsSuccessCode = false };
            }
            return await res.Content.ReadFromJsonAsync<ErrorDto>() ?? new ErrorDto();
        }

        public async Task<ErrorDto> DeleteStorageSlot(int slotId)
        {
            // TODO: Implement
            var res = await _http.DeleteAsync(configuration.ApiEndpoints.DeleteStorageSlot + "?storageSlotId=" + slotId);
            if (!res.IsSuccessStatusCode)
            {
                return new ErrorDto() { Code = 1, ErrorText = "Unexpected error while adding Storageslot. See logs for further Information", IsSuccessCode = false };
            }
            return await res.Content.ReadFromJsonAsync<ErrorDto>() ?? new ErrorDto();
        }

        public async Task<ErrorDto> UpdateSuggestion(SuggestionDTO item)
        {
            var res = await _http.PutAsJsonAsync(configuration.ApiEndpoints.UpdateSuggestion, item);
            if (!res.IsSuccessStatusCode)
            {
                return new ErrorDto() { Code = 1, ErrorText = "Unexpected error while updating Suggestion. See logs for further Information", IsSuccessCode = false };
            }
            return await res.Content.ReadFromJsonAsync<ErrorDto>() ?? new ErrorDto();
        }

        public Task<ErrorDto?> DeleteSuggestion(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserStatsDto> GetUserStats(string username)
        {
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetUserStats + "?username=" + username);
            if (!res.IsSuccessStatusCode)
            {
                return new();
            }
            else
            {
                return await res.Content.ReadFromJsonAsync<UserStatsDto>() ?? new UserStatsDto();
            }
        }

        public async Task<List<ShortCategoryDto>> GetAllCategoriesShort()
        {
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetAllCategoriesShort);
            return await res.Content.ReadFromJsonAsync<List<ShortCategoryDto>>() ?? new ();
        }

        public async Task<List<PaymentDto>> GetAllPaymentsFiltered(DateTime? fromDate = null, DateTime? toDate = null, ShortCategoryDto? category = null, double? minAmount = null, double? maxAmount = null)
        {
            QueryBuilder query = new ();
            if (fromDate != null)
                query.Add(nameof(fromDate), fromDate?.ToString("yyyy-MM-dd"));
            
            if (toDate != null)
                query.Add(nameof(toDate), toDate?.ToString("yyyy-MM-dd"));

            if (category != null && category.Name != null)
                query.Add(nameof(category), category?.Name);

            if (minAmount != null && minAmount != 0)
                query.Add(nameof(minAmount), minAmount?.ToString());

            if (maxAmount != null && maxAmount != 0)
                query.Add(nameof(maxAmount), maxAmount?.ToString());

            var queryString = query.ToQueryString();

            var res = await _http.GetAsync(configuration.ApiEndpoints.GetAllPaymentsFiltered + queryString);
            if (!res.IsSuccessStatusCode) return new();
            return await res.Content.ReadFromJsonAsync<List<PaymentDto>>() ?? new();
        }

        public async Task<List<TopUpDto>> GetAllTopUpsFiltered(string username, DateTime? fromDate = null, DateTime? toDate = null, string? executor = null, double? amount = null)
        {
            QueryBuilder query = new()
            {
                { nameof(username), username }
            };
            if (fromDate != null)
                query.Add(nameof(fromDate), fromDate?.ToString("yyyy-MM-dd"));

            if (toDate != null)
                query.Add(nameof(toDate), toDate?.ToString("yyyy-MM-dd"));

            if (executor != null)
                query.Add(nameof(executor), executor);

            if (amount != null && amount != 0)
                query.Add(nameof(amount), amount?.ToString());

            var queryString = query.ToString();
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetAllTopupsFiltered + queryString);
            if (!res.IsSuccessStatusCode) return new();
            return await res.Content.ReadFromJsonAsync<List<TopUpDto>>() ?? new();
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var res = await _http.GetAsync(configuration.ApiEndpoints.GetRoles);
            if (!res.IsSuccessStatusCode) return new();
            return await res.Content.ReadFromJsonAsync<List<RoleDto>>() ?? new();
        }

        public async Task<ErrorDto> AddTopUp(double topUpAmount, string username, string exectuorName)
        {            
            var topUp = new AddTopupDto { CashAmount = topUpAmount, Username = username, ExectuorName = exectuorName };
            var res = await _http.PostAsJsonAsync(configuration.ApiEndpoints.AddTopUp, topUp);
            return await res.Content.ReadFromJsonAsync<ErrorDto>() ?? new();
        }

        public async Task<ErrorDto> UpdateArticle(ArticleDto article)
        {
            var res = await _http.PutAsJsonAsync(configuration.ApiEndpoints.UpdateArticle, article);
            return await res.Content.ReadFromJsonAsync<ErrorDto>() ?? new();
        }

        public async Task<ErrorDto?> DeleteArticle(int id)
        {
            var res = await _http.DeleteAsync(configuration.ApiEndpoints.DeleteArticle + "?id=" + id);
            return await res.Content.ReadFromJsonAsync<ErrorDto>();
        }

        #endregion
    }
}
