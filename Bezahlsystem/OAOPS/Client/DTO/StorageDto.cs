using OAOPS.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Client.DTO
{
    public class StorageDto
    {
        public StorageDto()
        {
            StorageName = string.Empty;
        }

        public StorageDto(StorageVM storageVM)
        {
            StorageName = storageVM.StorageName;
        }

        public string StorageName { get; set; }
        public string ImageData { get; set; } = string.Empty;

        public override string ToString()
        {
            return StorageName;
        }
    }
}
