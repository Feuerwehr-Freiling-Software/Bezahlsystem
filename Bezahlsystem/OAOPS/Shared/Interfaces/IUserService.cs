using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Interfaces
{
    public interface IUserService
    {
        public Task<double> GetUserBalance(string username);
    }
}
