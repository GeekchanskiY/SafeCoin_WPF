using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Course_project.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Crypto> Cryptos { get; set; } = null!;

        public DbSet<Shot> Shots { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<CryptoSub> CryptoSubs { get; set; } = null!;

        public ApplicationContext() {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasKey(x => x.Id);
            modelBuilder.Entity<Crypto>().HasKey(x => x.Id);
            modelBuilder.Entity<Shot>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<CryptoSub>().HasKey(x => x.Id);
            

            modelBuilder.Entity<Review>()
              .HasOne(r => r.User)
              .WithMany(us => us.Reviews)
              .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
              .HasOne(r => r.Crypto)
              .WithMany(c => c.Reviews)
              .HasForeignKey(r => r.CryptoId);

            modelBuilder.Entity<Shot>()
                .HasOne(s => s.Crypto)
                .WithMany(c => c.Shots)
                .HasForeignKey(s => s.CryptoId);

            modelBuilder.Entity<CryptoSub>()
                .HasOne(s => s.Crypto)
                .WithMany(c => c.CryptoSubs)
                .HasForeignKey(s => s.CryptoId);

            modelBuilder.Entity<CryptoSub>()
                .HasOne(s => s.User)
                .WithMany(u => u.CryptoSubs)
                .HasForeignKey(s => s.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
