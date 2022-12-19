using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class BoughtArticle
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PriceId { get; set; }
        public int Amount { get; set; }
        public byte[] ImageData { get; set; } = null!;
        public int Type { get; set; }
        public int? PaymentId { get; set; }
        public bool? Active { get; set; }
        public bool? IsInVending { get; set; }
        public int VendingMachineNumber { get; set; }
        public int VendingSlot { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual Price Price { get; set; } = null!;
    }
}
