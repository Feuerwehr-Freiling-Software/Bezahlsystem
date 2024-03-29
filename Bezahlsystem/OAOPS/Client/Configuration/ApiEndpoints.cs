﻿using Microsoft.AspNetCore.Http;

namespace OAOPS.Client.Configuration
{
    public class ApiEndpoints
    {
        public string BaseUri { get; set; } = string.Empty;

        #region Suggestions
        public string AddSuggestion { get; set; } = string.Empty;
        public string GetAllSuggestions { get; set; } = string.Empty;
        #endregion

        #region ErrorCodes

        public string GetAllErrors { get; set; } = string.Empty;
        public string AddError { get; set; } = string.Empty;

        #endregion
        #region Logs

        #endregion
        #region Articles

        public string GetAllArticles { get; set; } = string.Empty;
        public string Pay { get; set; } = string.Empty;
        public string GetArticlesFiltered { get; set; } = string.Empty;
        public string UpdateArticle { get; set; } = string.Empty;
        public string DeleteArticle { get; set; } = string.Empty;
        #endregion

        #region Storage

        public string GetAllStorages { get; set; } = string.Empty;
        public string AddStorage { get; set; } = string.Empty;
        public string GetStorageById { get; set; } = string.Empty;

        #endregion

        #region Slots

        public string GetSlotsOfStorageByName { get; set; } = string.Empty;
        public string UpdateStorageSlot { get; set; } = string.Empty;
        public string GetCategories { get; set; } = string.Empty;
        public string UpdateCategory { get; set; } = string.Empty;
        public string DeleteCategory { get; set; } = string.Empty;
        public string AddArticle { get; set; } = string.Empty;
        public string GetAllCategories { get; set; } = string.Empty;

        #endregion

        #region Users

        public string GetBalance { get; set; } = string.Empty;
        public string GetAllUsers { get; set; } = string.Empty;
        public string GetUsersFiltered { get; set; } = string.Empty;
        public string DeactivateUser { get; set; } = string.Empty;
        public string UpdateUser { get; set; } = string.Empty;
        public string AddStorageSlot { get; set; } = string.Empty;
        public string DeleteStorageSlot { get; set; } = string.Empty;
        public string UpdateSuggestion { get; set; } = string.Empty;
        public string GetUserStats { get; set; } = string.Empty;
        public string GetAllCategoriesShort { get; set; } = string.Empty;
        public string GetAllPaymentsFiltered { get; set; } = string.Empty;
        public string GetAllTopupsFiltered { get; set; } = string.Empty;
        public string GetRoles { get; set; } = string.Empty;
        public string AddTopUp { get; set; } = string.Empty;

        #endregion
    }
}
