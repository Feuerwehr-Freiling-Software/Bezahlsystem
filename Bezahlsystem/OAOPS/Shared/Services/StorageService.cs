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
        // private readonly IEmailService emailService;

        public StorageService(ApplicationDbContext db, IErrorCodeService codeService /*,IEmailService emailService*/)
        {
            Db = db;
            this.codeService = codeService;
            //this.emailService = emailService;
        }

        public ApplicationDbContext Db { get; }

        public int AddStorage(StorageDto storage)
        {
            var fStorage = Db.Storages.FirstOrDefault(x => x.StorageName == storage.StorageName);
            if (fStorage != null) return 31;

            var newStorage = new Storage() { StorageName = storage.StorageName, ImageData = storage.ImageData };
            Db.Storages.Add(newStorage);
            var res = Db.SaveChanges();

            if (res <= 0) return 32;
            if (res == 1) return 30;
            else return 33;
        }

        public int AddStorageSlot(StorageSlotDto storageSlot)
        {
            var fSlot = Db.Slots.FirstOrDefault(x => x.Name == storageSlot.SlotName);
            if (fSlot != null) return 41;
            var fStorage = Db.Storages.FirstOrDefault(x => x.StorageName == storageSlot.StorageName);
            if (fStorage == null) return 44;

            var newSlot = new Slot
            {
                Name = storageSlot.SlotName,
                StorageId = fStorage.Id,
                Storage = null
            };

            var tmp = Db.Slots.Add(newSlot);

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

        public int DeleteStorageSlot(int storageSlotId)
        {
            var fSlot = Db.Slots.FirstOrDefault(x => x.Id == storageSlotId);
            if (fSlot == null) return 41;

            Db.Slots.Remove(fSlot);
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
                               StorageName = storage.StorageName,
                               ImageData = storage.ImageData
                           };

            return storages.ToList();
        }

        public List<StorageSlotDto>? GetSlotsOfStorageByName(string name)
        {
            var slots = from slot in Db.Slots
                        join storage in Db.Storages on slot.StorageId equals storage.Id
                        where storage.StorageName == name
                        join articleInSlot in Db.ArticleInStorageSlots on slot.Id equals articleInSlot.SlotId into gj
                        from articleInSlot in gj.DefaultIfEmpty()
                        select new StorageSlotDto
                        {
                            ArticleName = articleInSlot != null ? articleInSlot.Article.Name : "",
                            MinAmount = articleInSlot != null ? articleInSlot.MinAmount : 0,
                            QuantityAtStart = articleInSlot != null ? articleInSlot.QuantityAtStart : 0,
                            SlotId = slot.Id,
                            SlotName = slot.Name,
                            StorageConnectionId = storage.ConnectionId,
                            StorageId = storage.Id,
                            StorageName = storage.StorageName
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

        public async Task<bool> NewArticleOrdered(int slot, string connectionId, string? username = null)
        {
            var fMachine = Db.Storages.FirstOrDefault(x => x.ConnectionId == connectionId);
            if (fMachine == null) return false;

            var fSlot = Db.Slots.FirstOrDefault(x => x.Storage.Id == fMachine.Id && x.Id == slot);
            if (fSlot == null) return false;

            var fArticle = Db.ArticleInStorageSlots.Include(x => x.Slot).ThenInclude(x => x.Storage).Include(x => x.Article).FirstOrDefault(x => x.Slot.StorageId == fMachine.Id);
            if (fArticle == null) return false;

            if (fArticle.QuantityActual > 0)
            {                
                fArticle.QuantityActual -= 1;
            }

            Db.ArticleInStorageSlots.Update(fArticle);
            await Db.SaveChangesAsync();

            if(fArticle.QuantityActual < fArticle.MinAmount)
            {
                // TODO: send mail to admin
                //await emailService.SendArticleAlmostEmptyMail(fMachine.StorageName, fSlot.Name, fArticle.QuantityActual, fArticle.Article.Name);
            }
            else if (fArticle.QuantityActual == 0)
            {
                // TODO: send mail to admin
                //await emailService.SendArticleEmptyMail(fMachine.StorageName, fSlot.Name, fArticle.Article.Name);
            }

            return true;
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
