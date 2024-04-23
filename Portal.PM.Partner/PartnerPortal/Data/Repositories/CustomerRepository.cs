using System.Collections.Generic;
using System.Linq;
using Acme.Models.QuickBooks;
using Dapper;

namespace Acme.Data.Repositories
{
    public class CustomerRepository : BaseRepository
    {
        public List<Customer> GetCustomers()
        {
            return WithConnection<List<Customer>>(connection =>
            {
                string query = "SELECT * FROM [QuickBooksCustomers]";
                return (connection.Query<Customer>(query)).ToList();
            });
        }

    }
}
