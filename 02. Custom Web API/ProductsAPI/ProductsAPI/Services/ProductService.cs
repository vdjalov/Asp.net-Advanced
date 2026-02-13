using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;

namespace ProductsAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext context;

        public ProductService(ProductDbContext _context)
        {
            this.context = _context;
        }

        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return this.context.Products.ToList();
        }

        public ActionResult<Product> GetProductById(int id)
        {
            return this.context.Products.Find(id);
        }
    }
}
