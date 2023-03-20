using CheckNGet.Models;
using CheckNGet.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CheckNGet.Controllers
{
    [Route("api/CheckNGet")]
    [ApiController]
    public class CheckNGetController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<UserDTO> GetRestaurant()
        {
            return new List<UserDTO>()
            {
            };
        }

    }
}
