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
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        public IActionResult GetRestaurants()
        {
            var restaurants = _mapper.Map<List<RestaurantDTO>>(_restaurantRepository.GetRestaurants());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurants);
        }

        [HttpGet("{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(Restaurant))]
        [ProducesResponseType(400)]
        public IActionResult GetRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var restaurant = _mapper.Map<RestaurantDTO>(_restaurantRepository.GetRestaurant(restaurantId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurant);
        }

        [HttpGet("{restaurantId}/dish")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dish>))]
        [ProducesResponseType(400)]
        public IActionResult GetDishByRestaurant(int restaurantId)
        {
            var items = _mapper.Map<List<DishDTO>>(_restaurantRepository.GetDishByRestaurant(restaurantId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRestaurant([FromBody] RestaurantDTO restaurantCreate)
        {
            if (restaurantCreate == null)
                return BadRequest(ModelState);

            var restaurant = _restaurantRepository.GetRestaurants()
                .Where(r => r.RestaurantName.Trim().ToUpper() == restaurantCreate.RestaurantName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (restaurant != null)
            {
                ModelState.AddModelError("", "Restaurant already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantMap = _mapper.Map<Restaurant>(restaurantCreate);

            if (!_restaurantRepository.CreateRestaurant(restaurantMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }
        [HttpPut("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRestaurant(int restaurantId, [FromBody] RestaurantDTO updateRestaurant)
        {
            if (updateRestaurant == null)
                return BadRequest(ModelState);

            if (restaurantId != updateRestaurant.Id)
                return BadRequest(ModelState);

            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantMap = _mapper.Map<Restaurant>(updateRestaurant);

            if (!_restaurantRepository.UpdateRestaurant(restaurantMap))
            {
                ModelState.AddModelError("", "Something went wrong updating restaurant!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
