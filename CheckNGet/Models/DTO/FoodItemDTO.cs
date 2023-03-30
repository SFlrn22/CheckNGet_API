namespace CheckNGet.Models.DTO
{
    public class FoodItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
    }
}
