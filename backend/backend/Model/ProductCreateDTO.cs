namespace backend.Model
{
    public class ProductCreateDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public bool? IsAvailable { get; set; }
    }
}