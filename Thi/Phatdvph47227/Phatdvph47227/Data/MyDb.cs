using Microsoft.EntityFrameworkCore;
using Phatdvph47227.Models;

namespace Phat_dvph47227.Data
{
    public class MyDb:DbContext
    {
        public MyDb(DbContextOptions options):base(options)
        {

        }
        public DbSet<CanHo> CanHos { get; set; }
        public DbSet<ToaNha> ToaNhas { get; set; }

    }
}
