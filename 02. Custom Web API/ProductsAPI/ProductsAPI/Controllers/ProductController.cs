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


        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            product = productService.CreateProduct(product.Name, product.Description);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        [HttpPut("{id}")]
        public ActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (this.productService.GetProductById(id) == null)
            {
                return NotFound();
            }

            this.productService.EditProduct(id, product);

            return NoContent();

        }


        [HttpPatch("{id}")]
        public IActionResult PatchProduct(int id, Product product)
        {
            if(this.productService.GetProductById(id) == null)
            {
                return NotFound();
            }

            this.productService.EditProductPartially(id, product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var productExists = this.productService.GetProductById(id);

            if (productExists == null)
            {
                return NotFound();
            }

            Product product = this.productService.DeleteProduct(id);

            return product;
        }

    }
}
