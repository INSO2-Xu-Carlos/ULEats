using backend.Core;
using backend.Model;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // POST /Restaurant
        [HttpPost]
        public IActionResult AddRestaurant([FromBody] RestaurantCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Restaurante inv�lido.");

            var result = _restaurantService.AddRestaurant(dto);
            if (result == null)
                return StatusCode(500, "Error al insertar el restaurante.");

            return Ok(result);
        }

        // GET /Restaurant/{id}
        [HttpGet("{id}")]
        public IActionResult GetRestaurantById(int id)
        {
            var restaurant = _restaurantService.GetRestaurantById(id);
            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        // GET /Restaurant
        [HttpGet]
        public IActionResult GetAllRestaurants()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }

        // PUT /Restaurant/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            if (restaurant == null || restaurant.RestaurantId != id)
                return BadRequest("Datos de restaurante inv�lidos.");

            var updated = _restaurantService.UpdateRestaurant(restaurant);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE /Restaurant/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            var deleted = _restaurantService.DeleteRestaurant(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}