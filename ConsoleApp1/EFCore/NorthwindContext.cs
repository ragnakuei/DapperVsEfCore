using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => base.OnConfiguring( optionsBuilder.UseSqlServer( ConfigReader.GetConnectionString("EfCore") ) );

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order>    Orders    { get; set; }
        public DbSet<Shipper>  Shippers  { get; set; }
    }
}