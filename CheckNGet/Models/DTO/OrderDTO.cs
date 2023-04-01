namespace CheckNGet.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = null!;
        public DateTime OrderDate { get; set; }
    }
}
