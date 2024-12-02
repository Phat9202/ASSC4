using ASS.Models;
using Microsoft.EntityFrameworkCore;

namespace ASS.Data
{
    public class MyDb:DbContext
    {
        public MyDb(DbContextOptions options):base(options) 
        {
            
        }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cart_Detail> Cart_Details { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
