using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class RunEfCore
    {
        private readonly NorthwindContext _northwindContext;

        public RunEfCore(bool isTracking)
        {
            var conneString    = ConfigReader.GetConnectionString("EfCore");
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>()
                                         .UseSqlServer(conneString)
                                         .UseQueryTrackingBehavior(isTracking 
                                                                       ? QueryTrackingBehavior.TrackAll 
                                                                       : QueryTrackingBehavior.NoTracking);
            _northwindContext = new NorthwindContext(optionsBuilder.Options);
        }

        public IEnumerable<Customer> GetCustomer()
        {
            return _northwindContext.Customers;
        }

        public IEnumerable<Order> GetOrder()
        {
            return _northwindContext.Orders
                                    .Include(o=>o.Customer)
                                    .Include(o=>o.ShippedBy);
        }
    }
}