using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Db
{
     
    public class OrdersDbContext : DbContext
    {
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public OrdersDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
