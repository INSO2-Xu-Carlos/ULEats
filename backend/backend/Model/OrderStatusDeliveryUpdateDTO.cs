namespace backend.Model
{
    public class OrderStatusDeliveryUpdateDTO
    {
        public string Status { get; set; } = null!;
        public int? DeliveryId { get; set; }
    }
}
