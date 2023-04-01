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
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrderDTO>>(_orderRepository.GetOrders());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }
        [HttpGet("{orderId}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            var order = _mapper.Map<OrderDTO>(_orderRepository.GetOrder(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }
        [HttpGet("{orderId}/user")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserFromOrder(int orderId) 
        {
            if(!_orderRepository.OrderExists(orderId))
                return NotFound();

            var user = _mapper.Map<UserDTO>(_orderRepository.GetUserFromOrder(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }
        [HttpGet("{orderId}/dish")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dish>))]
        [ProducesResponseType(400)]
        public IActionResult GetDishesFromOrder(int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            var user = _mapper.Map<List<DishDTO>>(_orderRepository.GetDishesFromOrder(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromQuery] int userId, [FromQuery] int dishId, [FromBody] OrderDTO orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);

            var order = _orderRepository.GetOrders()
                .Where(o => o.OrderCode.Trim().ToUpper() == orderCreate.OrderCode.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (order != null)
            {
                ModelState.AddModelError("", "Order already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderCreate);

            orderMap.User = _userRepository.GetUser(userId);

            if (!_orderRepository.CreateOrder(dishId, orderMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");

        }
    }
}
