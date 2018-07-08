using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}