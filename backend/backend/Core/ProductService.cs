using DataModel;
using LinqToDB;

namespace backend.Core
{
    public class ProductService
    {
        private UlEatsDb _context;

        public ProductService(UlEatsDb context)
        {
            _context = context;
        }

        public Product? AddProduct(Product product)
        {
            // Insert devuelve el número de filas afectadas, pero puedes usar InsertWithIdentity para obtener el ID generado
            var id = _context.InsertWithInt32Identity(product);
            if (id > 0)
            {
                product.ProductId = id;
                return product;
            }
            return null;
        }
    }
}
