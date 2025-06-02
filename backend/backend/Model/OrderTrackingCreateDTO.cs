namespace backend.Model
{
    public class OrderTrackingCreateDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = null!;
        public DateTimeOffset? ChangedAt { get; set; }
        public int? ChangedBy { get; set; }
        public string? Notes { get; set; }
    }
}