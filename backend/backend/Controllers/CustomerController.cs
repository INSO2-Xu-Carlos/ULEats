using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST /Customer
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Cliente inválido.");

            var result = _customerService.AddCustomer(customer);
            if (result == null)
                return StatusCode(500, "Error al insertar el cliente.");

            return Ok(result);
        }

        // GET /Customer/{id}
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // GET /Customer
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        // PUT /Customer/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.CustomerId != id)
                return BadRequest("Datos de cliente inválidos.");

            var updated = _customerService.UpdateCustomer(customer);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE /Customer/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var deleted = _customerService.DeleteCustomer(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}   
