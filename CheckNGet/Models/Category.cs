namespace CheckNGet.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryItem> CategoryItems { get; set; }

    }
}
