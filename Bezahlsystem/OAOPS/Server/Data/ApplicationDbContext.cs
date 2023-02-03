using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OAOPS.Shared.Models;

namespace OAOPS.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
            public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<OpenCheckout> OpenCheckouts { get; set; }
        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }
        public DbSet<User_has_Notification> User_Has_Notifications { get; set; }
        public DbSet<TopUp> TopUps { get; set; }
        public DbSet<UserBoughtArticleFromSlot> UserBoughtArticleFromSlots { get; set; }
        public DbSet<ArticleInStorageSlot> ArticleInStorageSlots { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ErrorCode> ErrorCodes { get; set; }
    }
}