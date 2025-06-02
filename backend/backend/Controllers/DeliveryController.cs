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
                return BadRequest("Datos de entrega inválidos.");

            var result = _deliveryService.AddDelivery(dto);
            if (result == null)
                return StatusCode(500, "Error al insertar la entrega.");

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