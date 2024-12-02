using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Test.Models;

namespace Test.Data
{
    public class MyDb:DbContext
    {
        public MyDb(DbContextOptions options):base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Product
            modelBuilder.Entity<Product>().
            HasKey(p => p.Id);
            modelBuilder.Entity<Product>().
            HasMany(p => p.ProductDetails).
            WithOne(pd => pd.Product).
            HasForeignKey(pd => pd.ProductId);
            //ProductDetail
            modelBuilder.Entity<ProductDetail>().
            HasKey(pd => pd.Id);
            // Cart
            modelBuilder.Entity<Cart>().
            HasKey(c => c.Id);
            modelBuilder.Entity<Cart>().
            HasOne(c => c.User).
            WithOne(u => u.Cart).
            HasForeignKey<Cart>(c => c.UserId).
            OnDelete(DeleteBehavior.Cascade); // Xóa
            modelBuilder.Entity<Cart>().
            HasMany(c => c.CartDetails).
            WithOne(cd => cd.Cart).
            HasForeignKey(cd => cd.CartId);
            // CartDetail
            modelBuilder.Entity<CartDetail>().
            HasKey(cd => cd.Id);
            modelBuilder.Entity<CartDetail>().
            HasOne(cd => cd.ProductDetail).
            WithMany().
            HasForeignKey(cd => cd.ProductDetailId);
            // Order
            modelBuilder.Entity<Order>().
            HasKey(o => o.Id);
            modelBuilder.Entity<Order>().
            HasMany(o => o.OrderDetails).
            WithOne(from => from.Order).
            HasForeignKey(from => from.OrderId);
            // OrderDetail
            modelBuilder.Entity<OrderDetail>().HasKey(od => od.Id);
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.ProductDetail)
                .WithMany()
                .HasForeignKey(od => od.ProductDetailId);
            // Account
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            // Thiết lập mối quan hệ one-to-one với Cart
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cart khi user bị xóa
            modelBuilder.Entity<Account>()
                .HasMany(a => a.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);
            // Role
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.Roleld);
            // UserRole
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.Roleld });
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
    }
}

