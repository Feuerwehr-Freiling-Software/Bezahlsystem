namespace OAOPS.Shared.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ApplicationDbContext _db;

        public LoggerService(ApplicationDbContext db)
        {
            _db = db;
        }

        public int AddLog(LogSeverity severity, string text, int errorCodeId, string? sender = null)
        {
            var log = new Log()
            {
                Date = DateTime.Now,
                Severity = severity,
                Text = text,
                Sender = sender,
                ErrorCodeId = errorCodeId
            };

            _db.Logs.Add(log);
            var res = _db.SaveChanges();

            if (res <= 0) return 81;
            else if (res == 1) return 80;
            else if (res > 1) return 83;

            return 0;
        }

        public int DeleteLog(int id)
        {
            var fLog = _db.Logs.FirstOrDefault(x => x.Id == id);
            if (fLog == null) return 82;

            _db.Logs.Remove(fLog);
            var res = _db.SaveChanges();
            if (res <= 0) return 81;
            else if (res == 1) return 80;
            else if (res > 1) return 83;

            return 0;
        }

        public List<Log> GetLogs()
        {
            var res = _db.Logs.ToList();
            return res;
        }

        public List<Log> GetLogsInDays(int days)
        {
            var startDate = DateTime.Now.AddDays(days * (-1));
            var now = DateTime.Now;

            var res = _db.Logs.Where(x => (x.Date > startDate) && (x.Date < now)).ToList();
            return res;
        }

        public List<Log> GetLogsInMonths(int months)
        {
            var startDate = DateTime.Now.AddMonths(months * (-1));
            var now = DateTime.Now;

            var res = _db.Logs.Where(x => (x.Date > startDate) && (x.Date < now)).ToList();
            return res;
        }

        public List<Log> GetLogsInWeeks(int weeks)
        {
            var days = weeks * 7;
            var res = GetLogsInDays(days);
            return res;
        }
    }
}
