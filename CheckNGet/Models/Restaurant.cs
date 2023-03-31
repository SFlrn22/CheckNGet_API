namespace CheckNGet.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string RestaurantAddress { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string ContactAddress { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public ICollection<RestaurantDish> RestaurantDishes { get; set; }
    }
}
