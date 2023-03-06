using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Price
    {
        public int Id { get; set; }
        public DateTime Since { get; set; }
        public DateTime? Until { get; set; }
        public double Amount { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
    }
}
