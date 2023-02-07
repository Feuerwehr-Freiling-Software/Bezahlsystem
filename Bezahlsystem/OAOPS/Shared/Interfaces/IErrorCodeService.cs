namespace OAOPS.Shared.Interfaces
{
    public interface IErrorCodeService
    {
        /// <summary>
        /// Checks if ErrorCode is Success Errorcode
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        /// <param name="fullName">Name of the Class + Method the Error occured in</param>
        /// <returns>Error text if it is an _error or empty string</returns>
        public string CheckErrorCode(int errorCode, string fullName);
        /// <summary>
        /// Gets Errortext corresponding with given ErrorCode
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        /// <returns>Error Text</returns>
        public string GetErrorText(int errorCode);

        /// <summary>
        /// Adds ErrorCode to Db
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        /// <returns>ErrorCode</returns>
        public int AddErrorCode(ErrorCode errorCode);

        /// <summary>
        /// Updates ErrorCode in _db
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        /// <returns>ErrorCode</returns>
        public int UpdateErrorCode(ErrorCode errorCode);
        /// <summary>
        /// Deletes ErrorCode from the Db
        /// </summary>
        /// <param name="errorCode">ErrorCode Id</param>
        /// <returns>Id</returns>
        public int DeleteErrorCode(int Id);

        /// <summary>
        /// Checks if the given Errorcode is a Success Errorcode
        /// </summary>
        /// <param name="errorCode">Errorcode that should be checked</param>
        /// <returns>if the Errorcode is success or not</returns>
        public bool IsSuccessErrorCode(int errorCode);

        /// <summary>
        /// Gets the ErrorCode Object from the Db
        /// </summary>
        /// <param name="errorCode">Error Code that identifies the Entity</param>
        /// <returns>ErrorCode object</returns>
        public ErrorCode GetError(int errorCode);

        /// <summary>
        /// Gets the ErrorCode Object from the Db and logs result in DB
        /// </summary>
        /// <param name="errorCode">Error Code that identifies the Entity</param>
        /// <returns>ErrorCode object</returns>
        public ErrorCode GetError(int res, string fullName);
    }
}
