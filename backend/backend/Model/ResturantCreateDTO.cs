namespace backend.Model
{
    public class RestaurantCreateDto
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Phone { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
    }
}
