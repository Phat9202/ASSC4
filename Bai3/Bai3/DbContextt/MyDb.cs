using Bai3.Models;
using Microsoft.EntityFrameworkCore;

namespace Bai3.DbContextt
{
    public class MyDb : DbContext
    {
        public MyDb(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Custumer> Custumers { get; set; }
    }
}
