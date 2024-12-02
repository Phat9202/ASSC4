using Microsoft.EntityFrameworkCore;
using Test2.Models;
namespace Test2.Context

{
    public class MyDb:DbContext
    {
        public MyDb(DbContextOptions options):base(options) 
        {

        }
        public virtual DbSet<Student> Students { get; set; }
    }
}
