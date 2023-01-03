using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Paymentsystem.Shared.Models;

namespace Paymentsystem.Shared.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Articlecategory> Articlecategories { get; set; } = null!;
        public virtual DbSet<Emailconfirmationcode> Emailconfirmationcodes { get; set; } = null!;
        public virtual DbSet<Errorcode> Errorcodes { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Notificationsubscription> Notificationsubscriptions { get; set; } = null!;
        public virtual DbSet<Opencheckout> Opencheckouts { get; set; } = null!;
        public virtual DbSet<Price> Prices { get; set; } = null!;
        public virtual DbSet<Refreshtoken> Refreshtokens { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<SlotInStorageHasArticle> SlotInStorageHasArticles { get; set; } = null!;
        public virtual DbSet<Storage> Storages { get; set; } = null!;
        public virtual DbSet<Suggestion> Suggestions { get; set; } = null!;
        public virtual DbSet<Topup> Topups { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserBoughtArticleFromSlot> UserBoughtArticleFromSlots { get; set; } = null!;
        public virtual DbSet<UserHasNotification> UserHasNotifications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySql("server=localhost;database=paymenstystem;user=root;password=Test1234", ServerVersion.Parse("10.4.24-mariadb"));
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OAOPS;Trusted_Connection=True;");
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Article>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.ArticleTypeId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //        entity.ToTable("article");

        //        entity.HasIndex(e => e.ArticleTypeId, "fk_Article_ArticleType1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.ArticleTypeId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("ArticleType_Id");

        //        entity.Property(e => e.Name).HasMaxLength(45);

        //        entity.HasOne(d => d.ArticleType)
        //            .WithMany(p => p.Articles)
        //            .HasForeignKey(d => d.ArticleTypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_Article_ArticleType1");
        //    });

        //    modelBuilder.Entity<Articlecategory>(entity =>
        //    {
        //        entity.ToTable("articlecategory");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedNever();

        //        entity.Property(e => e.Level).HasMaxLength(45);

        //        entity.Property(e => e.Name).HasMaxLength(45);
        //    });

        //    modelBuilder.Entity<Emailconfirmationcode>(entity =>
        //    {
        //        entity.ToTable("emailconfirmationcode");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedNever();

        //        entity.Property(e => e.ConfirmationCode).HasMaxLength(64);

        //        entity.Property(e => e.Created).HasColumnType("datetime");

        //        entity.Property(e => e.Expires).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<Errorcode>(entity =>
        //    {
        //        entity.ToTable("errorcode");

        //        entity.Property(e => e.Id).HasColumnType("int(11)");

        //        entity.Property(e => e.Code).HasColumnType("int(11)");

        //        entity.Property(e => e.ErrorText).HasMaxLength(50);

        //        entity.Property(e => e.IsSuccessErrorCode).HasColumnType("tinyint(4)");
        //    });

        //    modelBuilder.Entity<Log>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.ErrorCodeId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //        entity.ToTable("log");

        //        entity.HasIndex(e => e.ErrorCodeId, "fk_Log_ErrorCode1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.ErrorCodeId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("ErrorCode_Id");

        //        entity.Property(e => e.DateTime).HasColumnType("datetime");

        //        entity.Property(e => e.Sender).HasMaxLength(45);

        //        entity.Property(e => e.Severity).HasColumnType("int(11)");

        //        entity.Property(e => e.Text).HasMaxLength(200);

        //        entity.HasOne(d => d.ErrorCode)
        //            .WithMany(p => p.Logs)
        //            .HasForeignKey(d => d.ErrorCodeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_Log_ErrorCode1");
        //    });

        //    modelBuilder.Entity<Notificationsubscription>(entity =>
        //    {
        //        entity.ToTable("notificationsubscription");

        //        entity.Property(e => e.Id).HasColumnType("int(11)");

        //        entity.Property(e => e.Name).HasMaxLength(45);
        //    });

        //    modelBuilder.Entity<Opencheckout>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.UserId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //        entity.ToTable("opencheckout");

        //        entity.HasIndex(e => e.UserId, "fk_OpenCheckout_User1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.UserId)
        //            .HasMaxLength(64)
        //            .HasColumnName("User_Id");

        //        entity.Property(e => e.Date).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<Price>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.ArticleId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //        entity.ToTable("price");

        //        entity.HasIndex(e => e.ArticleId, "fk_Price_Article1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.ArticleId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("Article_Id");

        //        entity.Property(e => e.Since).HasColumnType("datetime");

        //        entity.Property(e => e.Until).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<Refreshtoken>(entity =>
        //    {
        //        entity.ToTable("refreshtoken");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("id");

        //        entity.Property(e => e.Created).HasColumnType("datetime");

        //        entity.Property(e => e.Expires).HasColumnType("datetime");

        //        entity.Property(e => e.Token).HasMaxLength(45);
        //    });

        //    modelBuilder.Entity<Slot>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.StorageId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //        entity.ToTable("slot");

        //        entity.HasIndex(e => e.StorageId, "fk_Slot_Storage1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.StorageId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("Storage_Id");

        //        entity.Property(e => e.Name).HasMaxLength(45);

        //        entity.HasOne(d => d.Storage)
        //            .WithMany(p => p.Slots)
        //            .HasForeignKey(d => d.StorageId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_Slot_Storage1");
        //    });

        //    modelBuilder.Entity<SlotInStorageHasArticle>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.ArticleId, e.SlotId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

        //        entity.ToTable("slot_in_storage_has_article");

        //        entity.HasIndex(e => e.ArticleId, "fk_Storage_has_Article_Article1_idx");

        //        entity.HasIndex(e => e.SlotId, "fk_Storage_has_Article_Slot1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.ArticleId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("Article_Id");

        //        entity.Property(e => e.SlotId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("Slot_Id");

        //        entity.Property(e => e.MinAmount).HasColumnType("int(11)");

        //        entity.Property(e => e.QuantityActual).HasColumnType("int(11)");

        //        entity.Property(e => e.QuantityAtStart).HasColumnType("int(11)");
        //    });

        //    modelBuilder.Entity<Storage>(entity =>
        //    {
        //        entity.ToTable("storage");

        //        entity.Property(e => e.Id).HasColumnType("int(11)");

        //        entity.Property(e => e.ConnectionId).HasMaxLength(45);

        //        entity.Property(e => e.StorageName).HasMaxLength(45);
        //    });

        //    modelBuilder.Entity<Suggestion>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.UserId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        //        entity.ToTable("suggestions");

        //        entity.HasIndex(e => e.UserId, "fk_Suggestions_User1_idx");

        //        entity.Property(e => e.Id).HasColumnType("int(11)");

        //        entity.Property(e => e.UserId)
        //            .HasMaxLength(64)
        //            .HasColumnName("User_Id");

        //        entity.Property(e => e.Importance).HasColumnType("int(11)");

        //        entity.Property(e => e.SuggestionText).HasMaxLength(200);
        //    });

        //    modelBuilder.Entity<Topup>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.UserId, e.ExecutorId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

        //        entity.ToTable("topup");

        //        entity.HasIndex(e => e.UserId, "fk_Topup_User1_idx");

        //        entity.HasIndex(e => e.ExecutorId, "fk_Topup_User2_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.UserId)
        //            .HasMaxLength(64)
        //            .HasColumnName("User_Id");

        //        entity.Property(e => e.ExecutorId)
        //            .HasMaxLength(64)
        //            .HasColumnName("Executor_Id");

        //        entity.Property(e => e.CashAmount).HasMaxLength(45);

        //        entity.Property(e => e.Date).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.RefreshtokenId, e.EmailConfirmationCodeId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

        //        entity.ToTable("user");

        //        entity.HasIndex(e => e.EmailConfirmationCodeId, "fk_User_EmailConfirmationCode1_idx");

        //        entity.HasIndex(e => e.RefreshtokenId, "fk_User_Refreshtoken_idx");

        //        entity.Property(e => e.Id)
        //            .HasMaxLength(64)
        //            .HasComment("64 weil sha256 lol\n");

        //        entity.Property(e => e.RefreshtokenId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("Refreshtoken_id");

        //        entity.Property(e => e.EmailConfirmationCodeId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("EmailConfirmationCode_Id");

        //        entity.Property(e => e.Comment).HasMaxLength(128);

        //        entity.Property(e => e.ConfirmedEmail).HasColumnType("tinyint(4)");

        //        entity.Property(e => e.Email).HasMaxLength(45);

        //        entity.Property(e => e.Firstname).HasMaxLength(45);

        //        entity.Property(e => e.IsConfirmedUser).HasColumnType("tinyint(4)");

        //        entity.Property(e => e.Lastname).HasMaxLength(45);

        //        entity.Property(e => e.PasswordHash).HasMaxLength(64);

        //        entity.Property(e => e.PasswordSalt).HasMaxLength(64);

        //        entity.Property(e => e.Role).HasMaxLength(45);

        //        entity.Property(e => e.Username).HasMaxLength(45);

        //        entity.HasOne(d => d.EmailConfirmationCode)
        //            .WithMany(p => p.Users)
        //            .HasForeignKey(d => d.EmailConfirmationCodeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_User_EmailConfirmationCode1");

        //        entity.HasOne(d => d.Refreshtoken)
        //            .WithMany(p => p.Users)
        //            .HasForeignKey(d => d.RefreshtokenId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_User_Refreshtoken");
        //    });

        //    modelBuilder.Entity<UserBoughtArticleFromSlot>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.UserId, e.SlotInStorageHasArticleId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

        //        entity.ToTable("user_bought_article_from_slot");

        //        entity.HasIndex(e => e.UserId, "fk_Article_has_User_User1_idx");

        //        entity.HasIndex(e => e.SlotInStorageHasArticleId, "fk_User_Bought_Article_Storage_has_Article1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.UserId)
        //            .HasMaxLength(64)
        //            .HasColumnName("User_Id");

        //        entity.Property(e => e.SlotInStorageHasArticleId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("Slot_in_Storage_has_Article_Id");

        //        entity.Property(e => e.Quantity).HasColumnType("int(11)");

        //        entity.Property(e => e.TimeBought).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<UserHasNotification>(entity =>
        //    {
        //        entity.HasKey(e => new { e.Id, e.UserId, e.NotificationId })
        //            .HasName("PRIMARY")
        //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

        //        entity.ToTable("user_has_notification");

        //        entity.HasIndex(e => e.NotificationId, "fk_User_has_Notification_Notification1_idx");

        //        entity.HasIndex(e => e.UserId, "fk_User_has_Notification_User1_idx");

        //        entity.Property(e => e.Id)
        //            .HasColumnType("int(11)")
        //            .ValueGeneratedOnAdd();

        //        entity.Property(e => e.UserId)
        //            .HasMaxLength(64)
        //            .HasColumnName("User_Id");

        //        entity.Property(e => e.NotificationId)
        //            .HasColumnType("int(11)")
        //            .HasColumnName("Notification_Id");

        //        entity.HasOne(d => d.Notification)
        //            .WithMany(p => p.UserHasNotifications)
        //            .HasForeignKey(d => d.NotificationId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_User_has_Notification_Notification1");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
