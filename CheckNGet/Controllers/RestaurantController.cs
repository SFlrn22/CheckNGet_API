using AutoMapper;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
using CheckNGet.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CheckNGet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public RestaurantController(IRestaurantRepository restaurantRepository,IDishRepository dishRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public IActionResult GetDishByRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var items = _mapper.Map<List<DishDTO>>(_restaurantRepository.GetDishByRestaurant(restaurantId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult CreateRestaurant([FromBody] RestaurantDTO restaurantCreate)
        {
            if (restaurantCreate == null)
                return BadRequest(ModelState);

            var restaurant = _restaurantRepository.CompareRestaurants(restaurantCreate);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        [HttpDelete("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult DeleteRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var dishesToDelete = _restaurantRepository.GetDishByRestaurant(restaurantId);
            var restaurantToDelete = _restaurantRepository.GetRestaurant(restaurantId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_dishRepository.DeleteDishes(dishesToDelete.ToList()));
            {
                ModelState.AddModelError("", "Something went wrong deleting dishes tied to restaurant");
            }

            if (!_restaurantRepository.DeleteRestaurant(restaurantToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting restaurant");
            }

            return NoContent();
        }
    }
}
