using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Interfaces
{
    public interface IErrorCodeService
    {
        public Errorcode? GetErrorcodeByCode(int Code);
        public List<Errorcode> GetAllErrors();
        public int AddError(Errorcode errorcode);
        public int UpdateError(Errorcode errorcode);
        public int DeleteError(int errorcode);
    }
}
