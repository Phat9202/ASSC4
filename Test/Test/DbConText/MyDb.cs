using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.DbConText
{
    public class MyDb : DbContext
    {
        public MyDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
