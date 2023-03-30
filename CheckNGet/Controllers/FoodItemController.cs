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
    public class FoodItemController : Controller
    {
        private readonly IFoodItemRepository _fooditemRepository;
        private readonly IMapper _mapper;
        public FoodItemController(IFoodItemRepository fooditemRepository, IMapper mapper)
        {
            _fooditemRepository = fooditemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItem>))]
        public IActionResult GetFoodItems()
        {
            var foodItems = _mapper.Map<List<FoodItemDTO>>(_fooditemRepository.GetFoodItems());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(foodItems);
        }

        [HttpGet("{fooditemId}")]
        [ProducesResponseType(200, Type = typeof(FoodItem))]
        [ProducesResponseType(400)]
        public IActionResult GetFoodItem(int fooditemId)
        {
            if (!_fooditemRepository.FoodItemExists(fooditemId))
                return NotFound();

            var foodItem = _mapper.Map<FoodItemDTO>(_fooditemRepository.GetFoodItem(fooditemId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(foodItem);
        }
    }
}
