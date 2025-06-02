using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            var created = _productService.CreateProduct(product);
            if (created == null)
                return BadRequest("Bad Request. We couldnt create a product");
            return Ok(created);
        }

        // GET: /Product/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // GET: /Product
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: /Product/ByRestaurant/{restaurantId}
        [HttpGet("ByRestaurant/{restaurantId}")]
        public IActionResult GetByRestaurant(int restaurantId)
        {
            var products = _productService.GetProductsByRestaurant(restaurantId);
            return Ok(products);
        }

        // PUT: /Product/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var updated = _productService.UpdateProduct(id, product);
            if (!updated)
                return BadRequest("Bad request. Product failed to update");
            return Ok("Product updated succesfully");
        }

        // DELETE: /Product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _productService.DeleteProduct(id);
            if (!deleted)
                return NotFound();
            return Ok("Product deleted succesfully");
        }
    }
}