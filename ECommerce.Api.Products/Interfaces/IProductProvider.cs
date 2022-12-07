using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {

        Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductByIdAsync(int Id);
    }
}
