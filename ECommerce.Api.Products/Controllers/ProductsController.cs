using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider productProvider;
        public ProductsController(IProductProvider productProvider)
        {
            this.productProvider = productProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var data = await this.productProvider.GetProductsAsync();
            if (data.IsSuccess)
            {
                return Ok(data.products);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var data = await this.productProvider.GetProductByIdAsync(id);
            if (data.IsSuccess)
            {
                return Ok(data.product);
            }
            return NotFound();
        }
    }
}
