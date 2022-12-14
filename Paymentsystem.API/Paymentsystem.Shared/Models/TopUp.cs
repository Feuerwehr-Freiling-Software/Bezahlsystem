using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class TopUp
    {
        public int TopUpId { get; set; }
        public string PersonId { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public double CashAmount { get; set; }
        public string ExecutorId { get; set; } = null!;

        public virtual User Executor { get; set; } = null!;
        public virtual User Person { get; set; } = null!;
    }
}
