using Microsoft.EntityFrameworkCore;
using Paymentsystem.Shared.Data;
using Paymentsystem.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Services
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _db;

        public TokenService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Refreshtoken GetTokenFromUserByName(string username)
        {
            var user = _db.Users.Include(x => x.Refreshtoken).FirstOrDefault(x => x.Username == username);
            return user.Refreshtoken;
        }

        public int UpdateTokenFromUserByName(string username, string refreshtoken, DateTime created, DateTime expires)
        {
            var user = _db.Users.Include(x => x.Refreshtoken).FirstOrDefault(x => x.Username == username);
            user.Refreshtoken.Expires = expires;
            user.Refreshtoken.Created = created;
            user.Refreshtoken.Token = refreshtoken;

            _db.Users.Update(user);
            var res = _db.SaveChanges();

            switch (res)
            {
                case <= 0:
                    return 54;
                case 1:
                    return 50;
                case > 1:
                    return 55;
            }
        }
    }
}
