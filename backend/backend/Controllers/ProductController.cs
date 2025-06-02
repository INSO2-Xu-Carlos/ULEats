using backend.Core;
using DataModel;
using Microsoft.AspNetCore.Mvc;

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
            if (product == null)
                return BadRequest("Producto inválido.");

            var result = _productService.AddProduct(product);
            if (result == null)
                return StatusCode(500, "Error al insertar el producto.");

            return Ok(result);
        }
    }
}
