using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CheckNGet.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DBContext _context;
        private readonly IConfiguration _config;
        public LoginRepository(DBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public User Authenticate(UserLoginDTO userLogin)
        {
            var currentUser = _context.Users.Where(cu => cu.UserName.Trim().ToUpper() == userLogin.UserName.TrimEnd().ToUpper() && cu.Password == userLogin.Password).FirstOrDefault();

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
