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

        public Product CreateProduct(string name, string description)
        {
           Product product = new Product
            {
                Name = name,
                Description = description
            };

            this.context.Products.Add(product);
            this.context.SaveChanges();

            return product;
        }

        public void EditProduct(int id, Product product)
        {
            Product dbProduct = this.context.Products.Find(id);

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            this.context.SaveChanges();
        }

        public void EditProductPartially(int id, Product product)
        {
            Product dbProduct = this.context.Products.Find(id);

            dbProduct.Name = String.IsNullOrEmpty(product.Name) ? dbProduct.Name : product.Name;
            dbProduct.Description = String.IsNullOrEmpty(product.Description) ? dbProduct.Description : product.Description;
            this.context.SaveChanges();

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
