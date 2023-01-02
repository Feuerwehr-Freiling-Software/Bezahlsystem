using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.ViewModels
{
    public class Enums
    {
        public enum Importance
        {
            High = 0,
            Medium = 1,
            Low = 2
        }

        public enum LogSeverity
        {
            Info,
            Warning,
            Error                
        }
    }
}
