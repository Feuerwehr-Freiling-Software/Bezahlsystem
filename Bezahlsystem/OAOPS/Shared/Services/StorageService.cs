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
            var slots = from articleInSlot in Db.ArticleInStorageSlots.Include(x => x.Article).Include(x => x.Slot).ThenInclude(x => x.Storage).ToList()
                       where articleInSlot.Slot.Storage.StorageName == name
                        join slot in Db.Slots.ToList() on articleInSlot.SlotId equals slot.Id into gj
                        from test in gj.DefaultIfEmpty()
                       select new StorageSlotDto
                       {
                           ArticleName = articleInSlot.Article.Name,
                           MinAmount = articleInSlot.MinAmount,
                           QuantityAtStart = articleInSlot.QuantityAtStart,
                           SlotId = test.Id,
                           SlotName = test.Name,
                           StorageConnectionId = test.Storage.ConnectionId,
                           StorageId = test.StorageId,
                           StorageName = test.Storage.StorageName
                       };

            var allSlotsQuery = from slot in Db.Slots.ToList()
                                join articleInSlot in Db.ArticleInStorageSlots
                                    .Include(x => x.Article)
                                    .Include(x => x.Slot)
                                    .ThenInclude(x => x.Storage)
                                on slot.Id equals articleInSlot.SlotId into gj
                                from articleInSlot in gj.DefaultIfEmpty() // Perform left join
                                group articleInSlot by slot.Name;

            var sourceQuery = allSlotsQuery
                .Where(group => group.Any(articleInSlot => articleInSlot != null && articleInSlot.Slot.Storage.StorageName.Equals(name)))
                .FirstOrDefault();

            if (sourceQuery == null || !sourceQuery.Any())
                return null;

            Dictionary<Slot, List<ArticleInStorageSlot>> dict = new();
            var allSlots = Db.Slots.ToList().Where(x => x.Storage.StorageName == name);
            // get all Slots of Storage and add to dict
            foreach (var item in allSlots)
            {
                List<ArticleInStorageSlot> articlesInSlot = new();
                articlesInSlot = Db.ArticleInStorageSlots.Where(x => x.SlotId == item.Id).ToList();
                dict.Add(item, articlesInSlot);
                Console.WriteLine($"{item.Name} Count: {articlesInSlot.Count}");
            }

            List<StorageSlotDto> resultList = new();

            foreach (var slot in dict)
            {
                if (slot.Value.Count <= 0)
                {
                    var storageSlot = new StorageSlotDto
                    {
                        ArticleName = "",
                        MinAmount = 0,
                        QuantityAtStart = 0,
                        ImageData = "",
                        SlotId = slot.Key.Id,
                        SlotName = slot.Key.Name,
                        StorageConnectionId = slot.Key.Storage.ConnectionId,
                        StorageId = slot.Key.Storage.Id,
                        StorageName = slot.Key.Storage.StorageName
                    };
                    resultList.Add(storageSlot);
                }
                else
                {
                    var tmpArticleInSlot = from article in slot.Value
                              select new StorageSlotDto
                              {
                                  ArticleName = article.Article.Name,
                                  MinAmount = article.MinAmount,
                                  QuantityAtStart = article.QuantityAtStart,
                                  ImageData = article.Article.Base64data,
                                  SlotId = slot.Key.Id,
                                  SlotName = slot.Key.Name,
                                  StorageConnectionId = slot.Key.Storage.ConnectionId,
                                  StorageId = slot.Key.Storage.Id,
                                  StorageName = slot.Key.Storage.StorageName
                              };

                    resultList.AddRange(tmpArticleInSlot);
                }
            }

            return resultList;
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
