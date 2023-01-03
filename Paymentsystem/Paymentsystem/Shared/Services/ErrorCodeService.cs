using Paymentsystem.Shared.Data;
using Paymentsystem.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Services
{
    public class ErrorCodeService : IErrorCodeService
    {
        private readonly ApplicationDbContext _db;

        public ErrorCodeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public int AddError(Errorcode errorcode)
        {
            if (_db.Errorcodes.Any(x => x.Code == errorcode.Code)) return 41;
            
            _db.Errorcodes.Add(errorcode);
            var res = _db.SaveChanges();

            switch (res)
            {
                case <= 0:
                    return 44;
                case 1:
                    return 40;
                case > 1:
                    return 45;
            }
        }

        public int DeleteError(int id)
        {
            if (!_db.Errorcodes.Any(x => x.Id == id)) return 42;

            _db.Errorcodes.Remove(_db.Errorcodes.First(x => x.Id == id)); 
            
            var res = _db.SaveChanges();
            switch (res)
            {
                case <= 0:
                    return 44;
                case 1:
                    return 40;
                case > 1:
                    return 45;
            }
        }

        public List<Errorcode> GetAllErrors()
        {
            return _db.Errorcodes.ToList();
        }

        public Errorcode? GetErrorcodeByCode(int code)
        {
            return _db.Errorcodes.FirstOrDefault(x => x.Code == code);
        }

        public int UpdateError(Errorcode errorcode)
        {
            if (!_db.Errorcodes.Any(x => x.Code == errorcode.Code)) return 46;

            _db.Errorcodes.Update(errorcode);
            var res = _db.SaveChanges();

            switch (res)
            {
                case <= 0:
                    return 44;
                case 1:
                    return 40;
                case > 1:
                    return 45;
            }
        }
    }
}
