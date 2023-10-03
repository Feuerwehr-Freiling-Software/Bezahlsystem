using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OAOPS.Shared.DTO;
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
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            // use AutoMapper to map the ApplicationUser from the Db to a List<UserDto>
            var users = from user in db.Users
                        select new UserDto
                        {
                            Balance = user.Balance,
                            Comment = user.Comment,
                            FirstName = user.FirstName,
                            IsConfirmedUser = user.IsConfirmedUser,
                            LastName = user.LastName,
                            Username = user.UserName,
                            Role = userManager.GetRolesAsync(GetUserByName(user.UserName)).Result.FirstOrDefault() ?? "No Role"
                        };

            return users.ToList();
        }

        public ApplicationUser GetUserByName(string username)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == username);
            return user;
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

        public async Task<List<UserDto>> GetUsersFiltered(string? username = null, int? page = null, int? pageSize = null)
        {
            var users = from user in db.Users.ToList()
                        select new UserDto
                        {
                            Balance = user.Balance,
                            Comment = user.Comment,
                            FirstName = user.FirstName,
                            IsConfirmedUser = user.IsConfirmedUser,
                            LastName = user.LastName,
                            Username = user.UserName,
                            Role = userManager.GetRolesAsync(GetUserByName(user.UserName)).Result.FirstOrDefault() ?? "No Role"
                        };
            
            if (page != null && pageSize != null) // page is 1 based
            {
                users = users.Skip((int)page * (int)pageSize).ToList();
            }

            if (username != null)
            {
                users = users.Where(x => x.Username.Contains(username)).ToList();
            }

            return users.ToList();
        }

        public async Task<UserStatsDto> GetUserStats(string username)
        {
            UserStatsDto userStats = new ();
            // Topup or Withdraw - Amount in month
            Dictionary<string, Dictionary<string, List<double>>> balance = new();
            Dictionary<string, int> articles = new();

            var result = from price in db.Prices.Include(x => x.Article).ToList()
                         join article in db.UserBoughtArticleFromSlots.Include(x => x.User).Include(x => x.ArticleInStorageSlot).ThenInclude(x => x.Article).ToList() on price.Article.Id equals article.ArticleInStorageSlot.Article.Id
                         where article.User != null && article.User.UserName == username
                         select new { article, price };

            var topups = from topup in db.TopUps.Include(x => x.User).ToList()
                        where topup.User != null
                              && topup.User.UserName == username
                        group topup by topup.Date.Month.ToString() into g
                        select g;

            Dictionary<string, List<double>> topupWithdraws = new();

            foreach (var item in topups)
            {
                var topupDoubles = item.ToList().Select(x => x.CashAmount).ToList();
                topupWithdraws.Add(item.Key, topupDoubles);
            }

            balance.Add("Aufladungen", topupWithdraws);


            foreach (var item in result)
            {
                // Fill Article Stats Dictionary
                var articleName = item.article.ArticleInStorageSlot.Article.Name;
                if (articles.TryGetValue(articleName, out int amount))
                {
                    articles[articleName] = amount + item.article.Quantity;
                }
                else
                {
                    articles[articleName] = item.article.Quantity;
                }

                // Fill Balance Stats Dictionary

            }

            userStats.ArticleStats = articles;
            userStats.BalanceStats = balance;

            return userStats;
        }
    }
}
