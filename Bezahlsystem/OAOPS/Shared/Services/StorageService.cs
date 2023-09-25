using Microsoft.EntityFrameworkCore;
using OAOPS.Shared.DTO;
using OAOPS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OAOPS.Shared.Services
{
    public class StorageService : IStorageService
    {
        private readonly IErrorCodeService codeService;

        public StorageService(ApplicationDbContext db, IErrorCodeService codeService)
        {
            Db = db;
            this.codeService = codeService;
        }

        public ApplicationDbContext Db { get; }

        public int AddStorage(StorageDto storage)
        {
            var fStorage = Db.Storages.FirstOrDefault(x => x.StorageName == storage.StorageName);
            if (fStorage != null) return 31;

            var newStorage = new Storage() { StorageName = storage.StorageName };
            Db.Storages.Add(newStorage);
            var res = Db.SaveChanges();

            if (res <= 0) return 32;
            if (res == 1) return 30;
            else return 33;
        }

        public int DeleteStorage(int id)
        {
            var fStorage = Db.Storages.FirstOrDefault(x => x.Id == id);
            if (fStorage == null) return 31;

            Db.Storages.Remove(fStorage);
            var res = Db.SaveChanges();

            if (res <= 0) return 32;
            if (res == 1) return 30;
            else return 33;
        }

        public List<StorageDto> GetAllStorages()
        {
            var storages = from storage in Db.Storages
                           select new StorageDto
                           {
                               StorageName = storage.StorageName
                           };

            return storages.ToList();
        }

        public List<StorageSlotDto>? GetSlotsOfStorageByName(string name)
        {
            var slots = from articleInSlot in Db.ArticleInStorageSlots.Include(x => x.Article).Include(x => x.Slot).ThenInclude(x => x.Storage).ToList()
                       where articleInSlot.Slot.Storage.StorageName == name
                       select new StorageSlotDto
                       {
                           ArticleName = articleInSlot.Article.Name,
                           MinAmount = articleInSlot.MinAmount,
                           QuantityAtStart = articleInSlot.QuantityAtStart,
                           SlotId = articleInSlot.SlotId,
                           SlotName = articleInSlot.Slot.Name,
                           StorageConnectionId = articleInSlot.Slot.Storage.ConnectionId,
                           StorageId = articleInSlot.Slot.StorageId,
                           StorageName = articleInSlot.Slot.Storage.StorageName
                       };

            return slots.ToList();
        }

        public StorageDto? GetStorageById(int id)
        {
            var storage = Db.Storages.FirstOrDefault(x => x.Id == id);
            return storage != null ? new StorageDto(storage) : null;
        }

        public StorageDto? GetStorageByName(string name)
        {
            var res = Db.Storages.Select(x => new StorageDto() { StorageName = x.StorageName, Id = x.Id}).FirstOrDefault(s => s.StorageName == name);
            return res;
        }

        public int UpdateStorageSlot(StorageSlotDto storageSlot)
        {
            var fSlot = Db.Slots.FirstOrDefault(x => x.Id == storageSlot.SlotId);
            if (fSlot == null) return 36;

            // TODO: add Article to slot
            var fArticle = Db.Articles.FirstOrDefault(x => x.Name == storageSlot.ArticleName);
            if (fArticle != null)
            {
                ArticleInStorageSlot articleInSlot = new()
                {
                    ArticleId = fArticle.Id,
                    MinAmount = storageSlot.MinAmount,
                    QuantityActual = storageSlot.QuantityAtStart,
                    QuantityAtStart = storageSlot.QuantityAtStart,
                    SlotId = storageSlot.SlotId
                };

                Db.ArticleInStorageSlots.Add(articleInSlot);
            }
            var fStorage = GetStorageByName(storageSlot.StorageName);

            if (fStorage != null && storageSlot.StorageName != fSlot.Storage.StorageName)
            {
                fSlot.StorageId = fStorage.Id;
            }

            fSlot.Name = storageSlot.SlotName;

            Db.Slots.Update(fSlot);

            var res = Db.SaveChanges();
            if (res <= 1) return 32;
            if (res == 2) return 30;
            else return 33;
        }
    }
}
