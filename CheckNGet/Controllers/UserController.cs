using AutoMapper;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
using CheckNGet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CheckNGet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var users = _mapper.Map<UserDTO>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("userName")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string userName)
        {
            if (!_userRepository.UserExists(userName))
                return NotFound();

            var users = _mapper.Map<UserDTO>(_userRepository.GetUser(userName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("{userId}/order")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersByUser(int userId)
        {
            var orders = _mapper.Map<List<OrderDTO>>(_userRepository.GetOrdersByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(orders);
        }
    }
}
