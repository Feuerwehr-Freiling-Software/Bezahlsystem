using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Paymentsystem.Shared.ViewModels.Enums;

namespace Paymentsystem.Shared.Interfaces
{
    public interface ILogService
    {
        public void AddLog(LogSeverity logSeverity, string errorText, string sender, int errorId);
        public List<Log> GetAllLogs();
        public Log? GetLog(int id);
    }
}
