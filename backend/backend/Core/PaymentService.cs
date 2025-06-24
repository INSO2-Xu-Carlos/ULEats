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

        /// <summary>
        /// Add a new Payment to the database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> The payment added if added correctly </returns>
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

        /// <summary>
        /// Get a Payment by its ID
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns> Payment with given id if any </returns>
        public Payment? GetPaymentById(int paymentId)
        {
            return _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
        }

        /// <summary>
        /// Get all Payments
        /// </summary>
        /// <returns> list with all payments </returns>
        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        /// <summary>
        /// Update an existing Payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns> true if updated correctly </returns>
        public bool UpdatePayment(Payment payment)
        {
            var updated = _context.Update(payment);
            return updated > 0;
        }

        /// <summary>
        /// Delete a Payment by its ID
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns> true if deleted correctly </returns>
        public bool DeletePayment(int paymentId)
        {
            var deleted = _context.Payments.Delete(p => p.PaymentId == paymentId);
            return deleted > 0;
        }
    }
}
