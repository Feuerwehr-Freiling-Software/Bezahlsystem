using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class UserBoughtArticleFromSlot
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public DateTime TimeBought { get; set; }
        public int Quantity { get; set; }
        public int SlotInStorageHasArticleId { get; set; }
        public SlotInStorageHasArticle SlotInStorageHasArticle { get; set; } = null!;
    }
}
