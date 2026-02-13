using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        ActionResult<IEnumerable<Product>> GetAllProducts();
        ActionResult<Product> GetProductById(int id);
    }
}
