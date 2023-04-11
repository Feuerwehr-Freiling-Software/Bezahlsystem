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

        #endregion
    }
}
