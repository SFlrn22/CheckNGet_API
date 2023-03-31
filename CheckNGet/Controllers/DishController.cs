using AutoMapper;
using CheckNGet.Interface;
using CheckNGet.Models.DTO;
using CheckNGet.Models;
using CheckNGet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CheckNGet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public DishController(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dish>))]
        public IActionResult GetDishes()
        {
            var dishes = _mapper.Map<List<DishDTO>>(_dishRepository.GetDishes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        [ProducesResponseType(200, Type = typeof(Dish))]
        [ProducesResponseType(400)]
        public IActionResult GetDish(int dishId)
        {
            if (!_dishRepository.DishExists(dishId))
                return NotFound();

            var dish = _mapper.Map<DishDTO>(_dishRepository.GetDish(dishId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dish);
        }
    }
}
