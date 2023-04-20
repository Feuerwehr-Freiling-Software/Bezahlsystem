using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<double> GetUserBalance(string username)
        {
            var user = db.Users.FirstOrDefault(x => x.NormalizedUserName == username.ToUpper());
            if (user == null)
            {
                return 0.0;
            }

            return user.Balance;
        }
    }
}
