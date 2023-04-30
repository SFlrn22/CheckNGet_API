using AutoMapper;
using CheckNGet.Controllers;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
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
    }
}
