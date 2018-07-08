using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ConsoleApp1.Common;
using Dapper;

namespace ConsoleApp1
{
    public class RunDapper
    {
        private readonly string _conneString;

        public RunDapper()
        {
            _conneString = ConfigReader.GetConnectionString("Dapper");
        }

        public IEnumerable<Customer> GetCustomer()
        {
            using (SqlConnection conn = new SqlConnection(_conneString))
            {
                return conn.Query<Customer>(@"SELECT * FROM Customers");
            }
        }

        public IEnumerable<Order> GetOrder()
        {
            using (SqlConnection conn = new SqlConnection(_conneString))
            {
                var result = conn.QueryMultiple(@"
SELECT * 
FROM Orders

SELECT * 
FROM Shippers s
WHERE EXISTS (
	SELECT 0
	FROM Orders o
	WHERE s.ShipperID = o.ShipVia
)

SELECT * 
FROM Customers c
WHERE EXISTS (
	SELECT 0
	FROM Orders o
	WHERE c.CustomerID = o.CustomerID
)
                ", new { CustomerID = "ALFKI", EmployeeID = 3 });
                
                var orders = result.Read<Order>();
                var shippers = result.Read<Shipper>()
                                         .ToDictionary(s => s.ShipperID);
                var customers = result.Read<Customer>()
                                      .ToDictionary(od => od.CustomerID);

                foreach (var order in orders)
                {
                    order.Customer = customers.GetValue(order.CustomerID);
                    
                    if(order.ShipVia.HasValue)
                        order.ShippedBy = shippers.GetValue(order.ShipVia.Value);
                }

                return orders;
            }
        }
    }
}