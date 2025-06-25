using Microsoft.AspNetCore.Mvc;
using backend.Model;
using LinqToDB;
using System.Linq;
using Microsoft.AspNetCore.Identity.Data;
using backend.Core;
using System.Text.RegularExpressions;
using DataModel;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly ClientService _clientService;
        public ClientController(ClientService clientService) => _clientService = clientService;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _clientService.LoginAndGetUser(request.Email, request.Password);
            if (user == null)
                return Unauthorized("Bad credentials");

            var customer = _clientService.GetCustomerByUserId(user.UserId);
            var restaurant = _clientService.GetRestaurantByUserId(user.UserId);
            var delivery = _clientService.GetDeliveryByUserId(user.UserId);

            return Ok(new
            {
                UserId = user.UserId,
                UserType = user.UserType,
                CustomerId = customer?.CustomerId,
                RestaurantId = restaurant?.RestaurantId,
                DeliveryId = delivery?.DeliveryId
            });
        }



        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request) {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.UserType))
            {
                return BadRequest("All filds must be filled!");
            }

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(request.Email, emailPattern))
            {
                return BadRequest("Email is not in a valid pattern");
            }
            if(request.Phone != null && request.Phone.Length != 9) 
            {
                return BadRequest("Phone must have 9 digits!");
            }
            if (request.Password.Length < 8)
            {
                return BadRequest("Password is weak, please enter more characters");
            }
            var user = _clientService.RegisterAndGetUser(request.FirstName, request.Password, request.Email, request.LastName, request.Phone, request.UserType);
            if (user == null)
                return BadRequest("Email is already registered");

            return Ok(new { UserId = user.UserId });
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string? Phone { get; set; }
            public string UserType { get; set; }
            public string LastName { get; set; }
        }

        
       
    }
}
