using CheckNGet.Interface;
using CheckNGet.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckNGet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            var user = _loginRepository.Authenticate(userLogin);
            if (user != null)
            {
                var token = _loginRepository.Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
    }
}
