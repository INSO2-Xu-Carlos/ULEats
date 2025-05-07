using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdertController : ControllerBase
    {

        private readonly ILogger<ClientController> _logger;

        public OrdertController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        
    }
}
