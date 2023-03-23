using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Client.DTO
{
    public class StorageSlotDto
    {
        public int StorageId { get; set; }
        public string StorageName { get; set; }
        public string? StorageConnectionId { get; set; } 
        public int SlotId { get; set; }
        public string SlotName { get; set; }
    }
}
