using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Models
{
    public class UserBoughtArticleFromSlot
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public DateTime TimeBought { get; set; }
        public int Quantity { get; set; }
        public int ArticleInStorageSlotId { get; set; }
        public ArticleInStorageSlot ArticleInStorageSlot { get; set; } = null!;
    }
}
