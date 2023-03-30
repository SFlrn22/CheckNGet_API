using CheckNGet.Interface;
using CheckNGet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckNGet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        public IActionResult GetRestaurants()
        {
            var restaurants = _restaurantRepository.GetRestaurants();

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

            var restaurant = _restaurantRepository.GetRestaurant(restaurantId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurant);
        }
    }
}
