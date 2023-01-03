using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Slot
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int StorageId { get; set; }

        public virtual Storage Storage { get; set; } = null!;
    }
}
