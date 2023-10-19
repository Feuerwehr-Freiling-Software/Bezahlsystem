using OAOPS.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Interfaces
{
    public interface IStorageService
    {
        public List<StorageDto> GetAllStorages();
        public int AddStorage(StorageDto storage);
        public StorageDto? GetStorageById(int id);
        public StorageDto? GetStorageByName(string name);
        public int DeleteStorage(int id);
        public List<StorageSlotDto>? GetSlotsOfStorageByName(string name);
        int UpdateStorageSlot(StorageSlotDto storageSlot);
        int AddStorageSlot(StorageSlotDto storageSlot);
        public int DeleteStorageSlot(int storageSlotId);
        public Task<bool> ConnectVendingMachine(string vendingMachineName, string connectionId);
        Task<bool> NewArticleOrdered(int slot, string connectionId);
    }
}
