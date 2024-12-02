using Microsoft.EntityFrameworkCore;
using Test3.Models;

namespace Test3.Context
{
    public class MyDb:DbContext
    {  
        public MyDb(DbContextOptions options):base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
    }
}
