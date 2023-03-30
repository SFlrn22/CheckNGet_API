namespace CheckNGet.Models.DTO
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string RestaurantAddress { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string ContactAddress { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
    }
}
