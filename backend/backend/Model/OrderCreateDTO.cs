using System.Text.Json.Serialization;

namespace backend.Model
{
    public class OrderCreateDTO
    {
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public int? DeliveryId { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public string DeliveryAddress { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset? EstimatedDeliveryTime { get; set; }
        [JsonPropertyName("actualDeliveryTime")]
        public DateTimeOffset? ActualDeliveryTime { get; set; }
        public string? SpecialInstructions { get; set; }
    }
}