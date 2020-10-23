using Microsoft.EntityFrameworkCore;
using ShopApi.DomainModels;


namespace ShopAPI.Data
{ 

    public class ShopDataContext : DbContext
    {
        public ShopDataContext(DbContextOptions<ShopDataContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}

