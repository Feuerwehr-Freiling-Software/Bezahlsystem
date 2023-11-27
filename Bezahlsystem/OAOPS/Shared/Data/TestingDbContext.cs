using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Data
{
    public class TestingDbContext : DbContext
    {
        public TestingDbContext(DbContextOptions<TestingDbContext> options) : base(options)
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

        public void DeleteCategory(ArticleCategory category)
        {
            var target = ArticleCategories
                        .Include(x => x.Children)
                        .FirstOrDefault(x => x.Id == category.Id);

            RecursiveDelete(target);

            SaveChanges();
        }

        private void RecursiveDelete(ArticleCategory parent)
        {
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
