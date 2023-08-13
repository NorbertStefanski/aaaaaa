using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class OrderManagementContext : DbContext
    {
        private readonly DbContextOptions _dbContextOptions;
        public DbSet<Bar> Bars { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Order> Orders { get; set; }

        public OrderManagementContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
