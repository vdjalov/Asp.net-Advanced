using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        Product CreateProduct(string name, string description);
        void EditProduct(int id, Product product);
        void EditProductPartially(int id, Product product);
        ActionResult<IEnumerable<Product>> GetAllProducts();
        ActionResult<Product> GetProductById(int id);
    }
}
