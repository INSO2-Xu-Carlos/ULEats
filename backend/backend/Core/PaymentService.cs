using backend.Model;
using DataModel;
using LinqToDB;

namespace backend.Core
{
    public class PaymentService
    {
        private readonly UlEatsDb _context;

        public PaymentService(UlEatsDb context)
        {
            _context = context;
        }

        // Crear un nuevo pago
        public Payment? AddPayment(PaymentCreateDto dto)
        {
            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus,
                TransactionId = dto.TransactionId,
                PaymentDay = dto.PaymentDay
            };

            var id = _context.InsertWithInt32Identity(payment);
            if (id > 0)
            {
                payment.PaymentId = id;
                return payment;
            }
            return null;
        }

        // Obtener pago por ID
        public Payment? GetPaymentById(int paymentId)
        {
            return _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
        }

        // Obtener todos los pagos
        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        // Actualizar pago
        public bool UpdatePayment(Payment payment)
        {
            var updated = _context.Update(payment);
            return updated > 0;
        }

        // Eliminar pago
        public bool DeletePayment(int paymentId)
        {
            var deleted = _context.Payments.Delete(p => p.PaymentId == paymentId);
            return deleted > 0;
        }
    }
}
