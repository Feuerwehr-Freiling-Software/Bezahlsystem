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
        public RoleManager<IdentityRole> RoleManager { get; set; }


        public UserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, ILogger<UserService> logger, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.logger = logger;
            RoleManager = roleManager;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            // TODO: use AutoMapper to map the ApplicationUser from the Db to a List<UserDto>
            var users = await db.Users
                .Select(user => new UserDto
                {
                    Balance = user.Balance,
                    Comment = user.Comment,
                    FirstName = user.FirstName,
                    IsConfirmedUser = user.IsConfirmedUser,
                    LastName = user.LastName,
                    Username = user.UserName,
                    Role = userManager.GetRolesAsync(GetUserByName(user.UserName)).Result.FirstOrDefault() ?? "No Role"
                })
                .ToListAsync();

            return users;
        }

        public ApplicationUser GetUserByName(string username)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == username);
            return user ?? new();
        }

        public async Task<double> GetUserBalance(string username)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == username.ToUpper());
            if (user == null)
            {
                return 0.0;
            }

            return user.Balance;
        }

        public async Task<List<UserDto>> GetUsersFiltered(string? username = null, int? page = null, int? pageSize = null)
        {
            var users = db.Users
                .Select(user => new UserDto
                {
                    Balance = user.Balance,
                    Comment = user.Comment,
                    FirstName = user.FirstName,
                    IsConfirmedUser = user.IsConfirmedUser,
                    LastName = user.LastName,
                    Username = user.UserName,
                    Role = userManager.GetRolesAsync(db.Users.FirstOrDefault(x => x.UserName == username) ?? new()).Result.FirstOrDefault() ?? "No Role"
                }).ToList();

            if (page != null && pageSize != null) // page is 1 based
            {
                users = users.Skip((int)page * (int)pageSize).ToList();
            }

            if (username != null && string.IsNullOrWhiteSpace(username))
            {
                users = users.Where(x => x.Username.Contains(username)).ToList();
            }

            return users;
        }

        public async Task<UserStatsDto> GetUserStats(string username)
        {
            UserStatsDto userStats = new();
            // Topup or Withdraw - Amount in month
            Dictionary<string, List<double>> balance = new();
            Dictionary<string, int> articles = new();

            var result = from price in db.Prices.Include(x => x.Article).ToList()
                         join article in db.UserBoughtArticleFromSlots.Include(x => x.User).Include(x => x.ArticleInStorageSlot).ThenInclude(x => x.Article).ToList() on price.Article.Id equals article.ArticleInStorageSlot.Article.Id
                         where article.User != null && article.User.UserName == username
                         select new { article, price };

            var topups = from topup in await db.TopUps.Include(x => x.User).ToListAsync()
                         where topup.User != null
                               && topup.User.UserName == username
                         select topup;

            Dictionary<string, List<double>> topupWithdraws = new();

            List<double> doubles = new();
            List<double> payments = new();
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

            return await Task.Run(res.ToList);
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
            });

            return await Task.Run(res.ToList);
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var roles = await db.Roles.Select(x => new RoleDto() { Name = x.Name }).ToListAsync();
            return roles;
        }

        public async Task<ErrorDto?> UpdateUser(UserDto user, string executorId)
        {
            var fUser = db.Users.FirstOrDefault(x => x.UserName == user.Username);
            if (fUser == null)
            {
                // TODO: Check for correct code
                return new ErrorDto() { IsSuccessCode = false, Code = 51, ErrorText = "User not found" };
            }

            // check for balance
            if (fUser.Balance > user.Balance)
            {
                // Create Payment
                // Haha DB mog ds nd
                // workaround mit - TopUp ???
                // TODO: Rework
            }
            else
            {
                // Create Topup           
                var topup = new TopUp()
                {
                    CashAmount = Math.Round((user.Balance - fUser.Balance), 2),
                    Date = DateTime.Now,
                    ExecutorId = executorId,
                    UserId = fUser.Id
                };
                await db.TopUps.AddAsync(topup);
            }

            fUser.FirstName = user.FirstName;
            fUser.LastName = user.LastName;
            fUser.Balance = user.Balance;
            fUser.Comment = user.Comment;
            fUser.IsConfirmedUser = user.IsConfirmedUser;

            // Role check and add if neccessary
            var isInRole = await userManager.IsInRoleAsync(fUser, user.Role);
            var userRole = await userManager.GetRolesAsync(fUser);
            if (!isInRole)
            {
                await userManager.RemoveFromRoleAsync(fUser, userRole.FirstOrDefault());
                await userManager.AddToRoleAsync(fUser, user.Role);
            }

            db.Users.Update(fUser);
            await db.SaveChangesAsync();

            return new ErrorDto() { Code = 50, ErrorText = "Successful User Operation", IsSuccessCode = true };
        }

        public async Task<ErrorDto?> AddTopUp(AddTopupDto topUp)
        {
            // TODO: Implement the logic to add a topup to a user
            // You can use the provided topUp parameter to access the necessary information
            // Return an ErrorDto object indicating the success or failure of the operation
            // You can use the db context and other dependencies already available in the class

            // Example implementation:
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == topUp.Username);
            if (user == null)
            {
                return new ErrorDto { IsSuccessCode = false, Code = 404, ErrorText = "User not found" };
            }

            // get userid of executor
            var executor = await db.Users.FirstOrDefaultAsync(x => x.UserName == topUp.ExectuorName);

            var topup = new TopUp
            {
                UserId = user.Id,
                CashAmount = topUp.CashAmount,
                Date = DateTime.Now,
                ExecutorId = executor.Id
            };

            await db.TopUps.AddAsync(topup);
            await db.SaveChangesAsync();
            return new ErrorDto { IsSuccessCode = true, Code = 200, ErrorText = "Topup added successfully" };
        }
    }
}
