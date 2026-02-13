using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        Product CreateProduct(string name, string description);
        ActionResult<IEnumerable<Product>> GetAllProducts();
        ActionResult<Product> GetProductById(int id);
    }
}
