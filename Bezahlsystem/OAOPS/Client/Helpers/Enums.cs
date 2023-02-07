using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Client.Helpers
{
    public class Enums
    {
        public enum LogSeverity
        {
            Error = 1,
            Warning = 2,
            Information = 3,
            Debug = 4
        }
        public enum Importance
        {
            High = 1,
            Low = 2,
            Medium = 3,
            NOW = 4,
            NotSet = -1
        }
    }
}
