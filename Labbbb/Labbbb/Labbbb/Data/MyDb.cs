using Labbbb.Models;
using Microsoft.EntityFrameworkCore;

namespace Labbbb.Data
{
    public class MyDb:DbContext
    {
        public MyDb(DbContextOptions options):base(options) 
        {

        }
        public virtual DbSet<CanHo> CanHos { get; set; }
        public virtual DbSet<ToaNha> ToaNhas { get; set; }
    }
}
