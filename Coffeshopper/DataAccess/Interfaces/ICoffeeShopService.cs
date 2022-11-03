using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICoffeeShopService
    {
        public List<Coffeeshop> GetCoffeeshops();
        public Coffeeshop GetCoffeeshop(int id);
        public bool AddCoffeeshop(Coffeeshop coffeeshop);
    }
}
