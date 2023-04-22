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
            A.CallTo(() => _mapper.Map<List<UserDTO>>(users)).Returns(userList);

            var controller = new UserController(_userRepository, _mapper);

            var result = controller.GetUsers();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
