using System;
using Teretana.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Teretana
{
    class AppDBContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingFee> TrainingFees { get; set; }
        public DbSet<MembershipFee> MembershipFees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<MembershipType> MembershipTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Teretana.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Training>().HasKey(table => new {
                table.Id,
                table.TrainingDate
            });

            builder.Entity<PurchaseItem>().HasKey(table => new {
                table.productId,
                table.purchaseId
            });

        }

    }
}
