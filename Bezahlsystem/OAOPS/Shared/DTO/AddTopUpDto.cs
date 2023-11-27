using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.DTO
{
    public class AddTopupDto
    {
        public double CashAmount { get; set; }
        public string ExectuorName { get; set; }
        internal string Username { get; set; }
    }
}
