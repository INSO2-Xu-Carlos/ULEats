using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;

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