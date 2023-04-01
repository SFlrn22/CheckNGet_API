using AutoMapper;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CheckNGet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
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
    }
}
