using AutoMapper;
using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public IActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            var user = _loginRepository.Authenticate(userLogin);
            if(user != null)
            {
                var token = _loginRepository.Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
    }
}
