using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OAOPS.Shared.Services
{
    public class ErrorCodeService : IErrorCodeService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        public ErrorCodeService(ApplicationDbContext db, ILogger loggerService)
        {
            _db = db;
            _logger = loggerService;
        }

        public async Task<List<ErrorCode>> GetErrorsFromList(List<int> errors)
        {
            List<ErrorCode> retList = new();

            foreach (var item in errors)
            {
                var res = await _db.ErrorCodes.FirstOrDefaultAsync(x => x.Code == item);
                if(res != null)
                {
                    retList.Add(res);
                    if (res.IsSuccessCode == false)
                    {
                        _logger.LogError(res.ErrorText);
                    }
                }
            }

            return retList;
        }

        public string CheckErrorCode(int res, string fullName)
        {
            if (IsSuccessErrorCode(res))
            {
                var error = GetErrorText(res);
                _logger.LogError($"Error occured in {fullName} : {error}");
                return error;
            }

            return "";
        }

        public int AddErrorCode(ErrorCode errorCode)
        {
            var fError = _db.ErrorCodes.FirstOrDefault(x => x.Code == errorCode.Code);
            if (fError != null) return 62;

            _db.ErrorCodes.Add(errorCode);
            var res = _db.SaveChanges();

            if (res <= 0) return 61;
            else if (res == 1) return 60;
            else if (res > 1) return 63;

            // default weil .Net bissi stoopid
            return 0;
        }

        public int DeleteErrorCode(int Id)
        {
            var fError = _db.ErrorCodes.FirstOrDefault(x => x.Id == Id);
            if (fError == null) return 64;

            _db.ErrorCodes.Remove(fError);
            var res = _db.SaveChanges();

            if (res <= 0) return 61;
            else if (res == 1) return 60;
            else if (res > 1) return 63;

            // default weil .Net bissi stoopid
            return 0;
        }

        public string GetErrorText(int errorCode)
        {
            return _db.ErrorCodes.FirstOrDefault(x => x.Code == errorCode).ErrorText;
        }

        public bool IsSuccessErrorCode(int errorCode)
        {
            var fError = (_db.ErrorCodes.FirstOrDefault(x => x.Code == errorCode));
            if (fError == null) return false;

            if (fError.IsSuccessCode == true) return true;
            return false;
        }

        public int UpdateErrorCode(ErrorCode errorCode)
        {
            var fError = _db.ErrorCodes.FirstOrDefault(x => x.Id == errorCode.Id);
            if (fError == null) return 64;

            _db.ErrorCodes.Remove(fError);
            var res = _db.SaveChanges();

            if (res <= 0) return 61;
            else if (res == 1) return 60;
            else if (res > 1) return 63;

            // default weil .Net bissi stoopid
            return 0;
        }

        public ErrorCode GetError(int errorCode)
        {
            return _db.ErrorCodes.First(x => x.Code == errorCode);
        }

        public ErrorCode GetError(int res, string fullName)
        {
            var error = GetError(res);

            if (!error.IsSuccessCode)
            {
                _logger.LogError($"Error occured in {fullName} : {error.ErrorText}");
            }

            return error;
        }

        public List<ErrorCode> GetAllErrors()
        {
            return _db.ErrorCodes.ToList();
        }
    }
}
