using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleApp1
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext() { }
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => base.OnConfiguring(optionsBuilder.UseSqlServer(ConfigReader.GetConnectionString("EfCoreNoTracking")));

        //public DbSet<Customer> Customers { get; set; }
        public DbSet<Customer> DapperCustomers { get; set; }
    }
}