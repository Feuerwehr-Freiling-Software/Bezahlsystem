using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class CoffeeshopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext db;

        public CoffeeshopService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool AddCoffeeshop(Coffeeshop coffeeshop)
        {
            db.Coffeeshops.Add(coffeeshop);
            return db.SaveChanges() == 1;
        }

        public Coffeeshop GetCoffeeshop(int id)
        {
            return db.Coffeeshops.FirstOrDefault(x => x.Id == id);
        }

        public List<Coffeeshop> GetCoffeeshops()
        {
            return db.Coffeeshops.ToList();
        }
    }
}
