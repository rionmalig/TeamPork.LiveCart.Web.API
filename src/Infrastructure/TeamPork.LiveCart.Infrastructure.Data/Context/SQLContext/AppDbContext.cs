using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.App;
using TeamPork.LiveCart.Infrastructure.Data.Entities.LiveCart.Auth;

namespace TeamPork.LiveCart.Infrastructure.Data.Context.SQLContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRefreshTokenEntity> RefreshTokens { get; set; }
        
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<InvoiceItemEntity> InvoiceItems { get; set; }
    }
}
