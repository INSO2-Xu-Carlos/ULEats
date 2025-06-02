using backend.Core;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private ProductService _productService;

        private ILogger<ClientController> _logger;

        public ProductController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "Pipo")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
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
