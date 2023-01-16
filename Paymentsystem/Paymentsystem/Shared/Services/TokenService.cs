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
    }
}
