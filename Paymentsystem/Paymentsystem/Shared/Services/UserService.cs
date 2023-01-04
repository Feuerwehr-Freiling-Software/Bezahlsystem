﻿using Microsoft.EntityFrameworkCore;
using Paymentsystem.Shared.Data;
using Paymentsystem.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db) 
        {
            _db = db;
        }

        public int AddUser(User user)
        {
            if (_db.Users.Any(x => x.Username == user.Username)) return 32;
            if(_db.Users.Any(x => x.Email == user.Email)) return 33;

            _db.Users.Add(user);
            var res = _db.SaveChanges();

            switch (res)
            {
                case <= 0:
                    return 34;
                case 1:
                    return 30;
                case > 1:
                    return 35;
            }

        }

        public int DeleteUser(string id)
        {
            if (!_db.Users.Any(x => x.Id == id)) return 36;

            _db.Users.Remove(_db.Users.First(x => x.Id == id));
            var res = _db.SaveChanges();
            switch (res)
            {
                case <= 0:
                    return 34;
                case 1:
                    return 30;
                case > 1:
                    return 35;
            }
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public User? GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email == email);
        }

        public User? GetById(string id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        public User? GetByRefreshToken(string refreshtoken)
        {
            return _db.Users.Include(x => x.Refreshtoken).FirstOrDefault(x => x.Refreshtoken.Token == refreshtoken);
        }

        public User? GetByUsername(string username)
        {
            return _db.Users.Include(x => x.Refreshtoken).FirstOrDefault(x => x.Username == username);
        }

        public int UpdateUser(User user)
        {
            if (!_db.Users.Any(x => x.Username == user.Username)) return 36;

            _db.Users.Update(user);
            var res = _db.SaveChanges();

            switch (res)
            {
                case <= 0:
                    return 34;
                case 1:
                    return 30;
                case > 1:
                    return 35;
            }
        }
    }
}
