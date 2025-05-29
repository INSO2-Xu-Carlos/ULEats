using Microsoft.AspNetCore.Mvc;
using backend.Model;
using LinqToDB;
using System.Linq;
using Microsoft.AspNetCore.Identity.Data;
using backend.Core;
using System.Text.RegularExpressions;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly ClientService _clientService;
        //private readonly ILogger<ClientController> _logger;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            bool isValid = _clientService.login(request.Email, request.Password);
            if (!isValid) {
                return Unauthorized("Bad credentials"); 
            }

            return Ok("Login Succesful");
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

            if (request.Password.Length < 8)
            {
                return BadRequest("Password is weak, please enter more characters");
            }
            bool  success = _clientService.register(request.FirstName, request.Password, request.Email, request.LastName, request.Phone, request.UserType);
            if (!success) {
                return BadRequest("Email is already registered"); 
            }

            return Ok("Register Succesful!");
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
