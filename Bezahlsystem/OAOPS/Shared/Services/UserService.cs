using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OAOPS.Client.DTO;
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
        private readonly ILogger<UserService> logger;

        public UserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, ILogger<UserService> logger)
        {
            this.db = db;
            this.userManager = userManager;
            this.logger = logger;
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
            Dictionary<string, List<double>> balance = new();
            Dictionary<string, int> articles = new();

            var result = from price in db.Prices.Include(x => x.Article).ToList()
                         join article in db.UserBoughtArticleFromSlots.Include(x => x.User).Include(x => x.ArticleInStorageSlot).ThenInclude(x => x.Article).ToList() on price.Article.Id equals article.ArticleInStorageSlot.Article.Id
                         where article.User != null && article.User.UserName == username
                         select new { article, price };

            var topups = from topup in db.TopUps.Include(x => x.User).ToList()
                        where topup.User != null
                              && topup.User.UserName == username
                           select topup;

            Dictionary<string, List<double>> topupWithdraws = new();

            List<double> doubles = new List<double>();
            List<double> payments = new List<double> ();
            for (int month = 1; month <= 12; month++)
            {
                var monthlyTopUp = topups.Where(t => t.Date.Month == month && t.Date.Year == DateTime.Now.Year);
                doubles.Add(monthlyTopUp.Any() ? monthlyTopUp.Sum(x => x.CashAmount) : 0);

                var monthlyPayment = result.Where(x => x.article.TimeBought.Month == month && x.article.TimeBought.Year == DateTime.Now.Year);
                payments.Add(monthlyPayment.Any() ? monthlyPayment.Sum(x => x.article.Quantity * x.price.Amount) : 0);
            }

            balance.Add("Aufladungen", doubles);
            balance.Add("Abbuchungen", payments);
            

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
            }


            userStats.ArticleStats = articles;
            userStats.BalanceStats = balance;

            return userStats;
        }

        public async Task<List<PaymentDto>> GetAllPaymentsFiltered(DateTime? fromDate = null, DateTime? toDate = null, string? category = null, double? minAmount = null, double? maxAmount = null)
        {
            List<PaymentDto> res = new();
            try
            {
                res = (from payment in db.UserBoughtArticleFromSlots.Include(x => x.User).Include(x => x.ArticleInStorageSlot).ThenInclude(x => x.Article).ThenInclude(x => x.ArticleCategory).ToList()
                       join price in db.Prices.Include(x => x.Article).ToList().DefaultIfEmpty() on payment.ArticleInStorageSlot.Article.Id equals price.Article.Id
                       select new PaymentDto
                       {
                           Article = new()
                           {
                               Amount = payment.ArticleInStorageSlot.QuantityActual,
                               Category = payment.ArticleInStorageSlot.Article.ArticleCategory.Name,
                               Name = payment.ArticleInStorageSlot.Article.Name,
                               MinAmount = payment.ArticleInStorageSlot.MinAmount,
                               PriceAmount = price.Amount,
                               QuantityActual = 0,
                               Base64data = "",
                               QuantityAtStart = 0,
                               StorageName = "",
                               StorageSlot = ""
                           },
                           PaymentDate = payment.TimeBought,
                           Sum = payment.Quantity * price.Amount,
                           Username = payment.User.FirstName + " " + payment.User.LastName
                       }).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetAllPaymentsFiltered");
            }

            if (fromDate != null)
            {
                res = res.Where(x => x.PaymentDate >= fromDate).ToList();
            }

            if (toDate != null)
            {
                res = res.Where(x => x.PaymentDate <= toDate).ToList();
            }

            if (category != null)
            {
                res = res.Where(x => x.Article.Category == category).ToList();
            }

            if (minAmount != null)
            {
                res = res.Where(x => x.Sum >= minAmount).ToList();
            }

            if (maxAmount != null)
            {
                res = res.Where(x => x.Sum <= maxAmount).ToList();
            }

            return res.ToList();
        }

        public async Task<List<TopUpDto>> GetAllTopupsFiltered(string username, DateTime? fromDate, DateTime? toDate, string? executor, double? amount)
        {
            var topups = db.TopUps.Include(x => x.User).Where(x => x.User.UserName == username).ToList();

            if (fromDate != null)
            {
                topups = topups.Where(x => x.Date >= fromDate).ToList();
            }

            if (toDate != null)
            {
                topups = topups.Where(x => x.Date <= toDate).ToList();
            }

            if (executor != null)
            {
                topups = topups.Where(x => x.Executor.UserName == executor).ToList();
            }

            if (amount != null)
            {
                topups = topups.Where(x => x.CashAmount == amount).ToList();
            }

            var res = topups.Select(x => new TopUpDto
            {
                CashAmount = x.CashAmount,
                Date = x.Date,
                ExecutorName = x.Executor.FirstName + " " + x.Executor.LastName,
                Id = x.Id
            }).ToList();

            return res;
        }
    }
}
