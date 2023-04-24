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
    public class UserControllerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void UserController_GetUsers_ReturnOK()
        {
            var users = A.Fake<List<User>>();
            var userList = A.Fake<List<UserDTO>>();
            A.CallTo(() => _userRepository.GetUsers()).Returns(users);
            A.CallTo(() => _mapper.Map<List<UserDTO>>(users)).Returns(userList);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.GetUsers();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void UserController_GetUser_ReturnOk()
        {
            var user = A.Fake<User>();
            var userDto = A.Fake<UserDTO>();
            
            A.CallTo(() => _userRepository.UserExists(user.Id)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(user.Id)).Returns(user);
            A.CallTo(() => _mapper.Map<UserDTO>(user)).Returns(userDto);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.GetUser(user.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void UserController_GetUserByName_ReturnOk()
        {
            var user = A.Fake<User>();
            var userDto = A.Fake<UserDTO>();

            A.CallTo(() => _userRepository.UserExists(user.UserName)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(user.UserName)).Returns(user);
            A.CallTo(() => _mapper.Map<UserDTO>(user)).Returns(userDto);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.GetUser(user.UserName);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void UserController_GetOrdersByUser_ReturnOk()
        {
            var user = A.Fake<User>();
            var orders = A.Fake<List<Order>>();
            var ordersDTO = A.Fake<List<OrderDTO>>();

            A.CallTo(() => _userRepository.GetOrdersByUser(user.Id)).Returns(orders);
            A.CallTo(() => _mapper.Map<List<OrderDTO>>(orders)).Returns(ordersDTO);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.GetOrdersByUser(user.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
