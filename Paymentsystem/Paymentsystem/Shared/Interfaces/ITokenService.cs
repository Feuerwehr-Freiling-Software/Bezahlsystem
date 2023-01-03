using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Interfaces
{
    public interface ITokenService
    {
        public Refreshtoken GetTokenFromUserByName(string username);
        public int UpdateTokenFromUserByName(string username, string refreshtoken, DateTime created, DateTime expires);
    }
}
