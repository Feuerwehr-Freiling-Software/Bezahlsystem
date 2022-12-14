using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Payment
    {
        public Payment()
        {
            BoughtArticles = new HashSet<BoughtArticle>();
        }

        public int PaymentId { get; set; }
        public string PersonId { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public double CashAmount { get; set; }
        public string? ExecutorId { get; set; }

        public virtual User? Executor { get; set; }
        public virtual User Person { get; set; } = null!;
        public virtual ICollection<BoughtArticle> BoughtArticles { get; set; }
    }
}
