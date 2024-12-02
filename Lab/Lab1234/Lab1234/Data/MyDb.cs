using Lab1234.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1234.Data
{
    public class MyDB:DbContext
    {
        public MyDB(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product>  Products { get; set; }
    }
}
