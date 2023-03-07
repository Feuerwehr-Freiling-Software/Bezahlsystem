using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Interfaces
{
    public interface IPriceService
    {
        public Task<Price> GetPriceAsync(int id);
        public Task<int> AddPriceAsync(Price price);
    }
}
