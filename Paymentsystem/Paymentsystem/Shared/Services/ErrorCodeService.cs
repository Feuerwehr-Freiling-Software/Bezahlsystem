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
        private readonly ILogService logService;

        public ErrorCodeService(ApplicationDbContext db, ILogService logService)
        {
            _db = db;
            this.logService = logService;
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

        public Errorcode? GetErrorcode(int code, string sender)
        {
            var res = _db.Errorcodes.FirstOrDefault(x => x.Code == code);
            if (res == null) return null;
            if (!res.IsSuccessErrorCode)
            {
                logService.AddLog(ViewModels.Enums.LogSeverity.Error, res.ErrorText, sender, res.Id);
            }

            return res;
        }

        public Errorcode? GetErrorcode(int Code)
        {
            return _db.Errorcodes.FirstOrDefault(x => x.Code == Code);
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
