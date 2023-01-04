using Paymentsystem.Shared.Data;
using Paymentsystem.Shared.Interfaces;
using Paymentsystem.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext db;

        public LogService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddLog(Enums.LogSeverity logSeverity, string errorText, string sender, int errorId)
        {
            var log = new Log()
            {
                DateTime = DateTime.Now,
                ErrorCodeId = errorId,
                Sender = sender,
                Severity = logSeverity,
                Text = errorText
            };

            db.Logs.Add(log);
        }

        public List<Log> GetAllLogs()
        {
            return db.Logs.ToList();
        }

        public Log? GetLog(int id)
        {
            return db.Logs.FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
