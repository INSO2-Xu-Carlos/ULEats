using backend.Model;
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

        public Product? AddProduct(ProductCreateDto dto)
        {
            var product = new Product
            {
                RestaurantId = dto.RestaurantId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category,
                IsAvailable = dto.IsAvailable
            };
            if (dto.Price < 0) return null;
            var id = _context.InsertWithInt32Identity(product);
            if (id > 0)
            {
                product.ProductId = id;
                return product;
            }
            return null;
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

        public bool UpdateProduct(int id, ProductCreateDto dto)
        {
            var existing = _context.Products.Find(id);
            if (existing == null) return false;

            existing.RestaurantId = dto.RestaurantId;
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.ImageUrl = dto.ImageUrl;
            existing.Category = dto.Category;
            existing.IsAvailable = dto.IsAvailable;

            _context.Update(existing);
            return true;



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