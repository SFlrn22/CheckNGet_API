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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDish([FromQuery] int restaurantId, [FromQuery] int categoryId, [FromBody] DishDTO dishCreate)
        {
            if (dishCreate == null)
                return BadRequest(ModelState);

            var dish = _dishRepository.GetDishes()
                .Where(d=> d.Name.Trim().ToUpper() == dishCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (dish != null)
            {
                ModelState.AddModelError("", "Dish already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dishMap = _mapper.Map<Dish>(dishCreate);

            if (!_dishRepository.CreateDish(restaurantId, categoryId, dishMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }
        [HttpPut("{dishId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int dishId, [FromQuery] int restaurantId, [FromQuery] int categoryId, [FromBody] DishDTO updateDish)
        {
            if (updateDish == null)
                return BadRequest(ModelState);

            if (dishId != updateDish.Id)
                return BadRequest(ModelState);

            if (!_dishRepository.DishExists(dishId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dishMap = _mapper.Map<Dish>(updateDish);

            if (!_dishRepository.UpdateDish(restaurantId, categoryId, dishMap))
            {
                ModelState.AddModelError("", "Something went wrong updating dish!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{dishId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDish(int dishId)
        {
            if (!_dishRepository.DishExists(dishId))
                return NotFound();

            var dishToDelete = _dishRepository.GetDish(dishId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_dishRepository.DeleteDish(dishToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting dish");
            }

            return NoContent();
        }
    }
}
