using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.DTO
{
    public class TopUpDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double CashAmount { get; set; }
        public string ExecutorName { get; set; }
    }
}
