namespace OAOPS.Shared.Interfaces
{
    public interface ILoggerService
    {
        /// <summary>
        /// Adds a Log to the _db
        /// </summary>
        /// <param name="severity">Severity of the Log</param>
        /// <param name="text">More information about the Log</param>
        /// <param name="errorCodeId">Id of the ErrorCode that is attached to it</param>
        /// <param name="sender">Method that the Error occured in</param>
        /// <returns></returns>
        public int AddLog(LogSeverity severity, string text, int errorCodeId,string? sender = null);

        /// <summary>
        /// Gets all Logs from the Db
        /// </summary>
        /// <returns>List of Logs</returns>
        public List<Log> GetLogs();

        /// <summary>
        /// Deletes Log from the Db
        /// </summary>
        /// <param name="id">Id that is corresponding with Log in Db</param>
        /// <returns>Error Code</returns>
        public int DeleteLog(int id);

        /// <summary>
        /// Returns the Logs since given Days
        /// </summary>
        /// <param name="days">Days since today</param>
        /// <returns>List of Logs since given Days</returns>
        public List<Log> GetLogsInDays(int days);

        /// <summary>
        /// Returns the Logs since given Months
        /// </summary>
        /// <param name="weeks">Weeks since today</param>
        /// <returns>List of Logs since given Weeks</returns>
        public List<Log> GetLogsInWeeks(int weeks);

        /// <summary>
        /// Returns the logs since given Months
        /// </summary>
        /// <param name="months">Months since today</param>
        /// <returns>List of Logs since given Months</returns>
        public List<Log> GetLogsInMonths(int months);
    }
}
