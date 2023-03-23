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

        public StorageDto? GetStorageById(int id)
        {
            var storage = Db.Storages.FirstOrDefault(x => x.Id == id);
            return storage != null ? new StorageDto(storage) : null;
        }

        public StorageDto? GetStorageByName(string name)
        {
            var res = Db.Storages.Select(x => new StorageDto() { StorageName = x.StorageName}).FirstOrDefault(s => s.StorageName == name);
            return res;
        }
    }
}
