namespace backend.Model
{
    public class DeliveryCreateDTO
    {
        public string UserId { get; set; } = null!;
        public string? VehiclePlate { get; set; }
        public string? Phone { get; set; }
        public string? VehicleType { get; set; }
    }
}