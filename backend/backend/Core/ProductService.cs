using DataModel;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace backend.Core
{
    public class ProductService
    {
        private readonly UlEatsDb _context;

        public ProductService(UlEatsDb context)
        {
            _context = context;
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetProductsByRestaurant(int restaurantId)
        {
            return _context.Products.Where(p => p.RestaurantId == restaurantId).ToList();
        }

        public Product? CreateProduct(Product product)
        {
            var id = _context.InsertWithInt32Identity(product);
            if (id > 0)
            {
                product.ProductId = id;
                return product;
            }
            return null;
        }

        public bool UpdateProduct(int id, Product updated)
        {
            var affected = _context.Products
                .Where(p => p.ProductId == id)
                .Set(p => p.RestaurantId, updated.RestaurantId)
                .Set(p => p.Name, updated.Name)
                .Set(p => p.Description, updated.Description)
                .Set(p => p.Price, updated.Price)
                .Set(p => p.ImageUrl, updated.ImageUrl)
                .Set(p => p.Category, updated.Category)
                .Set(p => p.IsAvailable, updated.IsAvailable)
                .Update();
            return affected > 0;
        }

        public bool DeleteProduct(int id)
        {
            var affected = _context.Products
                .Where(p => p.ProductId == id)
                .Delete();
            return affected > 0;
        }
    }
}