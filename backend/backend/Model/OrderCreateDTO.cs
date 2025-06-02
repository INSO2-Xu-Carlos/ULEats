namespace backend.Model
{
    public class OrderCreateDTO
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int DeliveryId { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public string DeliveryAddress { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset? EstimatedDeliveryTime { get; set; }
        public DateTimeOffset? ActualDeliveyTime { get; set; }
        public string? SpecialInstructions { get; set; }
    }
}
