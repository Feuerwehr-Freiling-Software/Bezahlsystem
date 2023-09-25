using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.DTO
{
    public class StorageSlotDto
    {
        public int StorageId { get; set; }
        public string StorageName { get; set; } = string.Empty;
        public string? StorageConnectionId { get; set; } 
        public int SlotId { get; set; }
        public string SlotName { get; set; } = string.Empty;
        public string ArticleName { get; set; } = string.Empty;
        public int QuantityAtStart { get; set; }
        public int MinAmount { get; set; }
    }
}
