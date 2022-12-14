using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Paymentsystem.Shared.Models
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
        public virtual DbSet<BoughtArticle> BoughtArticles { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Price> Prices { get; set; } = null!;
        public virtual DbSet<TopUp> TopUps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OAOPS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Articles", "Identity");

                entity.HasIndex(e => e.PriceId, "IX_Articles_PriceId");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.ImageData).HasDefaultValueSql("(0x)");

                entity.Property(e => e.IsInVending)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.PriceId);
            });

            modelBuilder.Entity<BoughtArticle>(entity =>
            {
                entity.ToTable("BoughtArticles", "Identity");

                entity.HasIndex(e => e.PaymentId, "IX_BoughtArticles_PaymentId");

                entity.HasIndex(e => e.PriceId, "IX_BoughtArticles_PriceId");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsInVending)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.BoughtArticles)
                    .HasForeignKey(d => d.PaymentId);

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.BoughtArticles)
                    .HasForeignKey(d => d.PriceId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments", "Identity");

                entity.HasIndex(e => e.ExecutorId, "IX_Payments_ExecutorId");

                entity.HasIndex(e => e.PersonId, "IX_Payments_PersonId");

                //entity.HasOne(d => d.Executor)
                //    .WithMany(p => p.PaymentExecutors)
                //    .HasForeignKey(d => d.ExecutorId);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PaymentPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("Prices", "Identity");
            });

            modelBuilder.Entity<TopUp>(entity =>
            {
                entity.ToTable("TopUps", "Identity");

                entity.HasIndex(e => e.ExecutorId, "IX_TopUps_ExecutorId");

                entity.HasIndex(e => e.PersonId, "IX_TopUps_PersonId");

                entity.Property(e => e.ExecutorId).HasDefaultValueSql("(N'')");

                entity.HasOne(d => d.Executor)
                    .WithMany(p => p.TopUpExecutors)
                    .HasForeignKey(d => d.ExecutorId);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TopUpPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
