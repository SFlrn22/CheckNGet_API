using AutoMapper;
using CheckNGet.Controllers;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CheckNGet.Test.Controller
{
    public class DishControllerTests
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public DishControllerTests()
        {
            _dishRepository = A.Fake<IDishRepository>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void DishController_GetDishes_ReturnOk()
        {
            var dishes = A.Fake<List<Dish>>();
            var dishList = A.Fake<List<DishDTO>>();

            A.CallTo(() => _dishRepository.GetDishes()).Returns(dishes);
            A.CallTo(() => _mapper.Map<List<DishDTO>>(dishes)).Returns(dishList);

            var controller = new DishController(_dishRepository, _mapper);

            var result = controller.GetDishes();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void DishController_GetDish_ReturnOk()
        {
            var dish = A.Fake<Dish>();
            var dishId = dish.Id;
            var dishMap = A.Fake<DishDTO>();

            A.CallTo(() => _dishRepository.DishExists(dishId)).Returns(true);
            A.CallTo(() => _dishRepository.GetDish(dishId)).Returns(dish);
            A.CallTo(() => _mapper.Map<DishDTO>(dish)).Returns(dishMap);

            var controller = new DishController(_dishRepository, _mapper);

            var result = controller.GetDish(dishId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void DishController_CreateDish_ReturnsOk()
        {
            var restaurant = A.Fake<Restaurant>();
            var category = A.Fake<Category>();
            var dishCreate = A.Fake<DishDTO>();
            var restaurantId = restaurant.Id;
            var categoryId = category.Id;
            var dishMap = A.Fake<Dish>();

            A.CallTo(() => _dishRepository.CompareDishes(dishCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Dish>(dishCreate)).Returns(dishMap);
            A.CallTo(() => _dishRepository.CreateDish(restaurantId, categoryId, dishMap)).Returns(true);

            var controller = new DishController(_dishRepository, _mapper);

            var result = controller.CreateDish(restaurantId, categoryId, dishCreate);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Successfully created!");
        }
        [Fact]
        public void DishController_UpdateDish_ReturnsNoContent()
        {
            var dish = A.Fake<Dish>();
            var dishId = dish.Id;
            var restaurant = A.Fake<Restaurant>();
            var category = A.Fake<Category>();
            var dishUpdate = A.Fake<DishDTO>();
            var restaurantId = restaurant.Id;
            var categoryId = category.Id;
            var dishMap = A.Fake<Dish>();

            A.CallTo(() => _dishRepository.DishExists(dishId)).Returns(true);
            A.CallTo(() => _mapper.Map<Dish>(dishUpdate)).Returns(dishMap);
            A.CallTo(() => _dishRepository.UpdateDish(restaurantId, categoryId, dishMap)).Returns(true);

            var controller = new DishController(_dishRepository, _mapper);

            var result = controller.UpdateDish(dishId, restaurantId, categoryId, dishUpdate);

            A.CallTo(() => _dishRepository.UpdateDish(restaurantId, categoryId, dishMap)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
        [Fact]
        public void DishController_DeleteDish_ReturnsNoContent()
        {
            var dish = A.Fake<Dish>();
            var dishId = dish.Id;
            var dishToDelete = A.Fake<Dish>();

            A.CallTo(() => _dishRepository.DishExists(dishId)).Returns(true);
            A.CallTo(() => _dishRepository.GetDish(dishId)).Returns(dishToDelete);
            A.CallTo(() => _dishRepository.DeleteDish(dishToDelete)).Returns(true);

            var controller = new DishController(_dishRepository, _mapper);

            var result = controller.DeleteDish(dishId);

            A.CallTo(() => _dishRepository.GetDish(dishId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _dishRepository.DeleteDish(dishToDelete)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
