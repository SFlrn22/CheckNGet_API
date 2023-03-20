namespace CheckNGet.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public decimal ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public string ImgUrl { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}
