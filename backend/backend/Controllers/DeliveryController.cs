using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryService _deliveryService;

        public DeliveryController(DeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        // POST: /Delivery
        [HttpPost]
        public IActionResult Create([FromBody] Delivery delivery)
        {
            var created = _deliveryService.CreateDelivery(delivery);
            if (created == null)
                return BadRequest("Bad Request. We couldnt create a delivery");
            return Ok(created);
        }

        // GET: /Delivery/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var delivery = _deliveryService.GetDeliveryById(id);
            if (delivery == null)
                return NotFound();
            return Ok(delivery);
        }

        // GET: /Delivery
        [HttpGet]
        public IActionResult GetAll()
        {
            var deliveries = _deliveryService.GetAllDeliveries();
            return Ok(deliveries);
        }

        // PUT: /Delivery/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Delivery delivery)
        {
            var updated = _deliveryService.UpdateDelivery(id, delivery);
            if (!updated)
                return BadRequest("Bad request. Delivery failed to update");
            return Ok("Delivery updated succesfully");
        }

        // DELETE: /Delivery/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _deliveryService.DeleteDelivery(id);
            if (!deleted)
                return NotFound();
            return Ok("Delivery deleted succesfully");
        }
    }
}