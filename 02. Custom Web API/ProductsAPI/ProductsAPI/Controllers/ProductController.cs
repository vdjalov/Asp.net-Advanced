using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {

            return this.productService.GetAllProducts();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = this.productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
    }
}
