using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;
using backend.Model;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: /Order
        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Orden inválida.");

            var result = _orderService.AddOrder(dto);
            if (result == null)
                return StatusCode(500, "Error al insertar la orden.");

            return Ok(result);
        }

        // GET: /Order/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        // GET: /Order
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // PUT: /Order/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OrderCreateDTO dto)
        {
            var updated = _orderService.UpdateOrder(id, dto);
            if (!updated)
                return BadRequest("Bad request. Order failed to update");
            return Ok("Order updated succesfully");
        }

        // DELETE: /Order/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _orderService.DeleteOrder(id);
            if (!deleted)
                return NotFound();
            return Ok("Order deleted succesfully");
        }

        [HttpGet("byDelivery/{deliveryId}")]
        public IActionResult GetOrdersByDeliveryId(int deliveryId)
        {
            var orders = _orderService.GetOrdersByDeliveryId(deliveryId);
            return Ok(orders);
        }

        [HttpGet("byCustomer/{customerId}")]
        public IActionResult GetOrdersByCustomerId(int customerId)
        {
            var orders = _orderService.GetOrdersByCustomerId(customerId);
            return Ok(orders);
        }

        [HttpGet("ByOwner/{userId}")]
        public IActionResult GetOrdersByRestaurantUser(int userId)
        {
            var orders = _orderService.GetOrdersByRestaurantUser(userId);
            if(orders == null || !orders.Any())
            {
                return NotFound(new List<Order>());
            }

            return Ok(orders);
        }
        [HttpPut("{id}/status-delivery")]
        public IActionResult PutStatusAndDelivery(int id, [FromBody] OrderStatusDeliveryUpdateDTO dto)
        {
            var updated = _orderService.UpdateOrderStatusAndDelivery(id, dto.Status, dto.DeliveryId);
            if (!updated)
                return NotFound("No se pudo actualizar la orden.");
            return Ok("Orden actualizada correctamente.");
        }

    }
}