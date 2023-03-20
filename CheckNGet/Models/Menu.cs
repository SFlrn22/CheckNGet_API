namespace CheckNGet.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
