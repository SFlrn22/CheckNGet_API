namespace CheckNGet.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserType { get; set; } = null!;

    }
}
