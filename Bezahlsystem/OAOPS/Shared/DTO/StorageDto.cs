using OAOPS.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.DTO
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

        public StorageDto(Storage storage)
        {
            StorageName = storage.StorageName;
        }

        public int Id { get; set; }
        public string StorageName { get; set; }
        public string ImageData { get; set; }
    }
}
