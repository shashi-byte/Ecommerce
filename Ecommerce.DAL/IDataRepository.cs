using Ecommerce.Models;

namespace Ecommerce.DAL
{
    public interface IDataRepository
    {
        Task<List<Product>> GetAllProducts();
    }
}
