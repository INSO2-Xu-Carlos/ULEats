using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;

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

        // POST: /Restaurant
        [HttpPost]
        public IActionResult Create([FromBody] Restaurant restaurant)
        {
            var created = _restaurantService.CreateRestaurant(restaurant);
            if (created == null)
                return BadRequest("No se pudo crear el restaurante.");
            return Ok(created);
        }

        // GET: /Restaurant/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var restaurant = _restaurantService.GetRestaurantById(id);
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }

        // GET: /Restaurant
        [HttpGet]
        public IActionResult GetAll()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }

        // GET: /Restaurant/ByUser/{userId}
        [HttpGet("ByUser/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var restaurants = _restaurantService.GetRestaurantsByUser(userId);
            return Ok(restaurants);
        }

        // PUT: /Restaurant/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Restaurant restaurant)
        {
            var updated = _restaurantService.UpdateRestaurant(id, restaurant);
            if (!updated)
                return BadRequest("No se pudo actualizar el restaurante.");
            return Ok("Restaurante actualizado correctamente.");
        }

        // DELETE: /Restaurant/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _restaurantService.DeleteRestaurant(id);
            if (!deleted)
                return NotFound();
            return Ok("Restaurante eliminado correctamente.");
        }
    }
}