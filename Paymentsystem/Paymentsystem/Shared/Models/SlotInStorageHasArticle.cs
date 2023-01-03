using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class SlotInStorageHasArticle
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
        public int QuantityActual { get; set; }
        public int QuantityAtStart { get; set; }
        public int MinAmount { get; set; }
        public int SlotId { get; set; }
        public Slot Slot { get; set; } = null!;
    }
}
