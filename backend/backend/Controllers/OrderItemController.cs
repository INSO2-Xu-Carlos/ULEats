using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;
using backend.Model;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _orderItemService;

        public OrderItemController(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // POST /OrderItem
        [HttpPost]
        public IActionResult AddOrderItem([FromBody] OrderItemCreateDto dto)
        {
            if (dto == null)
                return BadRequest("OrderItem inválido.");

            var result = _orderItemService.AddOrderItem(dto);
            if (result == null)
                return StatusCode(500, "Error al insertar el OrderItem.");

            return Ok(result);
        }

        // GET /OrderItem/{id}
        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id)
        {
            var orderItem = _orderItemService.GetOrderItemById(id);
            if (orderItem == null)
                return NotFound();

            return Ok(orderItem);
        }

        // GET /OrderItem
        [HttpGet]
        public IActionResult GetAllOrderItems()
        {
            var orderItems = _orderItemService.GetAllOrderItems();
            return Ok(orderItems);
        }

        // PUT /OrderItem/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            if (orderItem == null || orderItem.OrderItemId != id)
                return BadRequest("Datos de OrderItem inválidos.");

            var updated = _orderItemService.UpdateOrderItem(orderItem);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE /OrderItem/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            var deleted = _orderItemService.DeleteOrderItem(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("byCustomer/{customerId}")]
        public IActionResult GetOrderItemsByCustomer(int customerId)
        {
            var items = _orderItemService.GetOrderItemsByCustomer(customerId);
            return Ok(items);
        }

        [HttpDelete("byCustomer/{customerId}")]
        public IActionResult DeleteOrderItemsByCustomerId(int customerId)
        {
            var deleted = _orderItemService.DeleteOrderItemsByCustomerId(customerId);
            if (!deleted)
                return NotFound("No se encontraron items para ese cliente.");
            return Ok("OrderItems eliminados correctamente.");
        }

    }
}
