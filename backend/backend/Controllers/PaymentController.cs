using Microsoft.AspNetCore.Mvc;
using backend.Core;
using DataModel;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // POST /Payment
        [HttpPost]
        public IActionResult AddPayment([FromBody] Payment payment)
        {
            if (payment == null)
                return BadRequest("Pago inválido.");

            var result = _paymentService.AddPayment(payment);
            if (result == null)
                return StatusCode(500, "Error al insertar el pago.");

            return Ok(result);
        }

        // GET /Payment/{id}
        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        // GET /Payment
        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentService.GetAllPayments();
            return Ok(payments);
        }

        // PUT /Payment/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] Payment payment)
        {
            if (payment == null || payment.PaymentId != id)
                return BadRequest("Datos de pago inválidos.");

            var updated = _paymentService.UpdatePayment(payment);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE /Payment/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var deleted = _paymentService.DeletePayment(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
