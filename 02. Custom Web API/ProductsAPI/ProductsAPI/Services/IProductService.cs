using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        Product CreateProduct(string name, string description);
        Product DeleteProduct(int id);
        void EditProduct(int id, Product product);
        void EditProductPartially(int id, Product product);
        ActionResult<IEnumerable<Product>> GetAllProducts();
        ActionResult<Product> GetProductById(int id); // TO DO try to return clean product without action result
    }
}
