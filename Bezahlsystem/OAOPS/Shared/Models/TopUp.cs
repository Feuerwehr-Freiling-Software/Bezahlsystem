using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Models
{
    public class TopUp
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double CashAmount { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string ExecutorId { get; set; } = null!;
        public ApplicationUser Executor { get; set; } = null!;
    }
}
