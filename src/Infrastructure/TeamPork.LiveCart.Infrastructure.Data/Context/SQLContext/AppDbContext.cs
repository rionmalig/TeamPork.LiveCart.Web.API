using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.Auth;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.ML;

namespace TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Business)
                .WithMany(b => b.Members)
                .HasForeignKey(u => u.BusinessSeqId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusinessEntity>()
                .HasOne(b => b.Owner)
                .WithOne(u => u.OwnedBusiness)
                .HasForeignKey<BusinessEntity>(b => b.OwnerSeqId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserProfileEntity> UserProfiles { get; set; }
        public DbSet<BusinessEntity> Businesses { get; set; }
        public DbSet<BusinessProfileEntity> BusinessProfiles { get; set; }
        public DbSet<UserRefreshTokenEntity> RefreshTokens { get; set; }
        
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<InvoiceItemEntity> InvoiceItems { get; set; }

        public DbSet<SaleForecastModelMetadataEntity> SaleForecastModelMetadatas { get; set; }
        public DbSet<BusinessInviteCodeEntity> BusinessInviteCodes { get; set; }

    }
}
