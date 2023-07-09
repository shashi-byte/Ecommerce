using Ecommerce.DAL;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcommerceController : ControllerBase
    {
        private readonly IDataRepository _datarepository;
        public EcommerceController(IDataRepository datarepository)
        {
            _datarepository = datarepository;
        }

        [HttpGet("ProductDetails")]
        public async Task<List<Product>> GetproductDetails()
        {
            var data = await _datarepository.GetAllProducts();
            return data;
        }
    }
}
