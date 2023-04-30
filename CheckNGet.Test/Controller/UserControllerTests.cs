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
        [Fact]
        public void UserController_CreateUser_ReturnOk()
        {
            var userCreate = A.Fake<UserCreateDTO>();
            var userMap = A.Fake<User>();

            A.CallTo(() => _userRepository.CompareUsers(userCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<User>(userCreate)).Returns(userMap);
            A.CallTo(() => _userRepository.CreateUser(userMap)).Returns(true);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.CreateUser(userCreate);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Successfully created!");
        }
        [Fact]
        public void UserController_UpdateUser_ReturnNoContent()
        {
            var user = A.Fake<User>();
            var userId = user.Id;
            var userToBeUpdated = A.Fake<User>();
            var updateUser = A.Fake<UserDTO>();

            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(userId)).Returns(userToBeUpdated);
            A.CallTo(() => _userRepository.UpdateUser(userToBeUpdated)).Returns(true);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.UpdateUser(userId, updateUser);

            A.CallTo(() => _userRepository.GetUser(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.UpdateUser(userToBeUpdated)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
        [Fact]
        public void UserController_DeleteUser_ReturnNoContent()
        {
            var user = A.Fake<User>();
            var userId = user.Id;
            var userToDelete = A.Fake<User>();

            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(userId)).Returns(userToDelete);
            A.CallTo(() => _userRepository.DeleteUser(userToDelete)).Returns(true);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.DeleteUser(userId);

            A.CallTo(() => _userRepository.GetUser(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.DeleteUser(userToDelete)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
