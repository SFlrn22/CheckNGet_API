using AutoMapper;
using CheckNGet.Controllers;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
using CheckNGet.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckNGet.Test.Controller
{
    public class RestaurantControllerTests
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public RestaurantControllerTests()
        {
            _restaurantRepository = A.Fake<IRestaurantRepository>();
            _dishRepository = A.Fake<IDishRepository>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void RestaurantController_GetRestaurants_ReturnOK()
        {
            var restaurants = A.Fake<List<Restaurant>>();
            var restaurantList = A.Fake<List<RestaurantDTO>>();

            A.CallTo(() => _restaurantRepository.GetRestaurants()).Returns(restaurants);
            A.CallTo(() => _mapper.Map<List<RestaurantDTO>>(restaurants)).Returns(restaurantList);

            var controller = new RestaurantController(_restaurantRepository, _dishRepository, _mapper);

            var result = controller.GetRestaurants();

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void RestaurantController_GetRestaurant_ReturnOK()
        {
            var restaurant = A.Fake<Restaurant>();
            var restaurantDTO = A.Fake<RestaurantDTO>();

            A.CallTo(() => _restaurantRepository.RestaurantExists(restaurant.Id)).Returns(true);
            A.CallTo(() => _restaurantRepository.GetRestaurant(restaurant.Id)).Returns(restaurant);
            A.CallTo(() => _mapper.Map<RestaurantDTO>(restaurant)).Returns(restaurantDTO);

            var controller = new RestaurantController(_restaurantRepository, _dishRepository, _mapper);

            var result = controller.GetRestaurant(restaurant.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void RestaurantController_GetDishByRestaurant_ReturnOk()
        {
            var restaurant = A.Fake<Restaurant>();
            var dishes = A.Fake<List<Dish>>();
            var dishList = A.Fake<List<DishDTO>>();

            A.CallTo(() => _restaurantRepository.RestaurantExists(restaurant.Id)).Returns(true);
            A.CallTo(() => _restaurantRepository.GetDishByRestaurant(restaurant.Id)).Returns(dishes);
            A.CallTo(() => _mapper.Map<List<DishDTO>>(dishes)).Returns(dishList);

            var controller = new RestaurantController(_restaurantRepository, _dishRepository, _mapper);

            var result = controller.GetDishByRestaurant(restaurant.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void RestaurantController_CreateRestaurant_ReturnOk()
        {
            var restaurantCreate = A.Fake<RestaurantDTO>();
            var restaurantMap = A.Fake<Restaurant>();

            A.CallTo(() => _restaurantRepository.CompareRestaurants(restaurantCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Restaurant>(restaurantCreate)).Returns(restaurantMap);
            A.CallTo(() => _restaurantRepository.CreateRestaurant(restaurantMap)).Returns(true);

            var controller = new RestaurantController(_restaurantRepository, _dishRepository, _mapper);

            var result = controller.CreateRestaurant(restaurantCreate);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void RestaurantController_UpdateRestaurant_ReturnNoContent()
        {
            var restaurant = A.Fake<Restaurant>();
            var restaurantId = restaurant.Id;
            var updateRestaurant = A.Fake<RestaurantDTO>();
            var restaurantMap = A.Fake<Restaurant>();

            A.CallTo(() => _restaurantRepository.RestaurantExists(restaurantId)).Returns(true);
            A.CallTo(() => _mapper.Map<Restaurant>(updateRestaurant)).Returns(restaurantMap);
            A.CallTo(() => _restaurantRepository.UpdateRestaurant(restaurantMap)).Returns(true);

            var controller = new RestaurantController(_restaurantRepository, _dishRepository, _mapper);

            var result = controller.UpdateRestaurant(restaurantId, updateRestaurant);

            A.CallTo(() => _restaurantRepository.UpdateRestaurant(restaurantMap)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
        [Fact]
        public void RestaurantController_DeleteRestaurant_ReturnNoContext()
        {
            var restaurant = A.Fake<Restaurant>();
            var restaurantId = restaurant.Id;
            var dishesToDelete = A.Fake<List<Dish>>();
            var restaurantToDelete = A.Fake<Restaurant>();

            A.CallTo(() => _restaurantRepository.RestaurantExists(restaurantId)).Returns(true);
            A.CallTo(() => _restaurantRepository.GetDishByRestaurant(restaurantId)).Returns(dishesToDelete);
            A.CallTo(() => _restaurantRepository.GetRestaurant(restaurantId)).Returns(restaurantToDelete);
            A.CallTo(() => _dishRepository.DeleteDishes(A<List<Dish>>.That.Matches(d => d.SequenceEqual(dishesToDelete)))).Returns(true);
            A.CallTo(() => _restaurantRepository.DeleteRestaurant(restaurantToDelete)).Returns(true);

            var controller = new RestaurantController(_restaurantRepository, _dishRepository, _mapper);

            var result = controller.DeleteRestaurant(restaurantId);

            A.CallTo(() => _restaurantRepository.GetDishByRestaurant(restaurantId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _restaurantRepository.GetRestaurant(restaurantId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _dishRepository.DeleteDishes(A<List<Dish>>.That.Matches(d => d.SequenceEqual(dishesToDelete)))).MustHaveHappenedOnceExactly();
            A.CallTo(() => _restaurantRepository.DeleteRestaurant(restaurantToDelete)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
