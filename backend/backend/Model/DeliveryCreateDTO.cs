namespace backend.Model
{
    public class DeliveryCreateDTO
    {
        public int UserId { get; set; }
        public string? VehiclePlate { get; set; }
        public string? Phone { get; set; }
        public string? VehicleType { get; set; }
    }
}