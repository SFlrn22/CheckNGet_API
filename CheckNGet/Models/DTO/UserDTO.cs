using System.Text.Json.Serialization;

namespace CheckNGet.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        [JsonIgnore]
        public string Role { get; set; } = "User";
    }
}
