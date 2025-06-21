using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;
using backend.Model;

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

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Producto inválido.");

            var result = _productService.AddProduct(dto);
            if (result == null)
                return StatusCode(500, "Error al insertar el producto.");

            return Ok(result);
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
        public IActionResult Update(int id, [FromBody] ProductCreateDto dto)
        {
            var updated = _productService.UpdateProduct(id, dto);
            if (!updated) return NotFound();

            return NoContent();
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