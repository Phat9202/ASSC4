using Bai4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bai4.DbContextt
{
    public class MyDb : DbContext
    {
        public MyDb(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Custumer> Custumers { get; set; }
    }
}
