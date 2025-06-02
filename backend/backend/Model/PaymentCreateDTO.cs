namespace backend.Model
{
    public class PaymentCreateDto
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public string? TransactionId { get; set; }
        public DateTimeOffset? PaymentDay { get; set; }
    }
}