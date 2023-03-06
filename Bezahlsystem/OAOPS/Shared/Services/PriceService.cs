using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Services
{
    public class PriceService : IPriceService
    {
        public PriceService(ApplicationDbContext db)
        {
            Db = db;
        }

        public ApplicationDbContext Db { get; }

        public async Task<int> AddPriceAsync(Price price)
        {
            Db.Prices.Add(price);
            var res = await Db.SaveChangesAsync();

            switch (res)
            {
                case < 1:
                    return 1;
                case 1: 
                    return 2;
                case > 1:
                    return 3;
            }
        }

        public Task<Price> GetPriceAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
