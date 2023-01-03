using System;
using System.Collections.Generic;

namespace Paymentsystem.Shared.Models
{
    public partial class Storage
    {
        public Storage()
        {
            Slots = new HashSet<Slot>();
        }

        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string StorageName { get; set; } = null!;

        public virtual ICollection<Slot> Slots { get; set; }
    }
}
