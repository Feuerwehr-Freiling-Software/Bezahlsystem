using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Topup
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CashAmount { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string ExecutorId { get; set; } = null!;
        public ApplicationUser Executor { get; set; } = null!;
    }
}
