using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderTrackingController : ControllerBase
    {
        private readonly OrderTrackingService _orderTrackingService;

        public OrderTrackingController(OrderTrackingService orderTrackingService)
        {
            _orderTrackingService = orderTrackingService;
        }

        // POST /OrderTracking
        [HttpPost]
        public IActionResult AddOrderTracking([FromBody] OrderTracking tracking)
        {
            if (tracking == null)
                return BadRequest("Tracking inválido.");

            var result = _orderTrackingService.AddOrderTracking(tracking);
            if (result == null)
                return StatusCode(500, "Error al insertar el tracking.");

            return Ok(result);
        }

        // GET /OrderTracking/{id}
        [HttpGet("{id}")]
        public IActionResult GetOrderTrackingById(int id)
        {
            var tracking = _orderTrackingService.GetOrderTrackingById(id);
            if (tracking == null)
                return NotFound();

            return Ok(tracking);
        }

        // GET /OrderTracking
        [HttpGet]
        public IActionResult GetAllOrderTrackings()
        {
            var trackings = _orderTrackingService.GetAllOrderTrackings();
            return Ok(trackings);
        }

        // PUT /OrderTracking/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateOrderTracking(int id, [FromBody] OrderTracking tracking)
        {
            if (tracking == null || tracking.TrackingId != id)
                return BadRequest("Datos de tracking inválidos.");

            var updated = _orderTrackingService.UpdateOrderTracking(tracking);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE /OrderTracking/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderTracking(int id)
        {
            var deleted = _orderTrackingService.DeleteOrderTracking(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}