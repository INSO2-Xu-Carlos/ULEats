using Microsoft.AspNetCore.Mvc;
using backend.Core;
using Microsoft.AspNetCore.Identity.Data;
using DataModel;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {

        private ClientService _clientService;

        public RestaurantController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("create")]
        public IActionResult CreateRestaurant([FromBody] CreateRestaurantRequest request)
        {
            var user = _clientService.getUserById(request.UserId);


            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.UserType.ToLower() != "restaurant")
            {
                return Unauthorized("Only users with rol 'restaurant' can create a new restaurant");
            }

            var newRestaurant = new Restaurant()
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                UserId = request.UserId,
                Description = request.Description,
                LogoUrl = request.Logo_url
            };

            return Ok("There is a new restaurant in town!");
        }

        public class CreateRestaurantRequest
        {
            public string Name { get; set; }
            public string Address {  get; set; }
            public string Phone { get; set; }
            public int UserId { get; set; }
            public string Description { get; set; }
            public string Logo_url { get; set; }
        }
    }
}
