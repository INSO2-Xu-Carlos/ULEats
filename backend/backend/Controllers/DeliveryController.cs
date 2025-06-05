using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;
using backend.Model;

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
        public IActionResult AddDelivery([FromBody] DeliveryCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Bad credentials");
            
            if (dto.VehicleType == "Car" || dto.VehicleType == "Motorbike")
            {      
                if (string.IsNullOrWhiteSpace(dto.VehiclePlate))
                {
                    return BadRequest(dto.VehicleType + " must have a vehicle plate!");
                }
            }
            else if (dto.VehicleType == "Bike" || dto.VehicleType == "Scooter")
            {
                if (!string.IsNullOrWhiteSpace(dto.VehiclePlate))
                {
                    return BadRequest(dto.VehicleType + " must not have a vehicle plate!");
                } 
                dto.VehiclePlate = null;
            }

            if (dto.Phone != null && dto.Phone.Length != 9)
            {
                return BadRequest("Phone must have 9 digits");
            }

            var result = _deliveryService.AddDelivery(dto);

            if (result == null)
                return StatusCode(500, "Error");
            return Ok(result);
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