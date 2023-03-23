namespace OAOPS.Client.Configuration
{
    public class ApiEndpoints
    {
        public string BaseUri { get; set; }

        #region Suggestions
        public string AddSuggestion { get; set; }
        public string GetAllSuggestions { get; set; }
        #endregion

        #region ErrorCodes

        public string? GetAllErrors { get; set; }
        public string? AddError { get; set; }

        #endregion
        #region Logs

        #endregion
        #region Articles

        public string GetAllArticles { get; set; }
        public string Pay { get; set; }

        #endregion

        #region Storage

        public string GetAllStorages { get; set; }
        public string AddStorage { get; set; }
        public string GetStorageById { get; set; }

        #endregion
    }
}
