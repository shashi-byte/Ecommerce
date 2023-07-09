using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL
{
    public class DataRepository: DatabaseConnection, IDataRepository
    {
        public DataRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (var db = GetDatabase())
            {
                const string sql = $"select * from ProductDetails";
                return new List<Product>(await db.FetchAsync<Product>(sql));
            }
        }
    }
}
