using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OAOPS.Shared.DTO;
using OAOPS.Shared.Models;

namespace OAOPS.Shared.Data
{

    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Suggestion> Suggestions { get; set; } = null!;
        public DbSet<OpenCheckout> OpenCheckouts { get; set; } = null!;
        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; } = null!;
        public DbSet<User_has_Notification> User_Has_Notifications { get; set; } = null!;
        public DbSet<TopUp> TopUps { get; set; } = null!;
        public DbSet<UserBoughtArticleFromSlot> UserBoughtArticleFromSlots { get; set; } = null!;
        public DbSet<ArticleInStorageSlot> ArticleInStorageSlots { get; set; } = null!;
        public DbSet<Slot> Slots { get; set; } = null!;
        public DbSet<Storage> Storages { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;
        public DbSet<Price> Prices { get; set; } = null!;
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<ErrorCode> ErrorCodes { get; set; } = null!;

        public void DeleteCategory(ArticleCategory category)
        {
            if (ArticleCategories == null) return;

            var target = ArticleCategories
                        .Include(x => x.Children)
                        .FirstOrDefault(x => x.Id == category.Id);

            if (target == null) return;

            RecursiveDelete(target);

            SaveChanges();
        }

        private void RecursiveDelete(ArticleCategory parent)
        {
            if(ArticleCategories == null) return;

            if (parent.Children != null)
            {
                var children = ArticleCategories
                    .Include(x => x.Children)
                    .Where(x => x.ParentId == parent.Id).ToList();

                foreach (var item in children)
                {
                    RecursiveDelete(item);
                }
            }

            ArticleCategories.Remove(parent);
        }
    }
}