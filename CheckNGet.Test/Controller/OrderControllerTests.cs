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
    public class OrderControllerTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public OrderControllerTests()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _dishRepository = A.Fake<IDishRepository>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void OrderController_GetOrders_ReturnOK()
        {
            var orders = A.Fake<List<Order>>();
            var orderList = A.Fake<List<OrderDTO>>();

            A.CallTo(() => _orderRepository.GetOrders()).Returns(orders);
            A.CallTo(() => _mapper.Map<List<OrderDTO>>(orders)).Returns(orderList);

            var controller = new OrderController(_orderRepository, _userRepository, _dishRepository, _mapper);

            var result = controller.GetOrders();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void OrderController_GetOrder_ReturnOK()
        {
            var order = A.Fake<Order>();
            var orderId = order.Id;
            var orderMapped = A.Fake<OrderDTO>();

            A.CallTo(() => _orderRepository.OrderExists(orderId)).Returns(true);
            A.CallTo(() => _orderRepository.GetOrder(orderId)).Returns(order);
            A.CallTo(() => _mapper.Map<OrderDTO>(order)).Returns(orderMapped);

            var controller = new OrderController(_orderRepository, _userRepository, _dishRepository, _mapper);

            var result = controller.GetOrder(orderId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void OrderController_GetUserFromOrder_ReturnOK()
        {
            var order = A.Fake<Order>();
            var orderId = order.Id;
            var user = A.Fake<User>();
            var userMapped = A.Fake<UserDTO>();

            A.CallTo(() => _orderRepository.OrderExists(orderId)).Returns(true);
            A.CallTo(() => _orderRepository.GetUserFromOrder(orderId)).Returns(user);
            A.CallTo(() => _mapper.Map<UserDTO>(user)).Returns(userMapped);

            var controller = new OrderController(_orderRepository, _userRepository, _dishRepository, _mapper);

            var result = controller.GetUserFromOrder(orderId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void OrderController_GetDishesFromOrder_ReturnOK()
        {
            var order = A.Fake<Order>();
            var orderId = order.Id;
            var dishes = A.Fake<List<Dish>>();
            var dishesMapped = A.Fake<List<DishDTO>>();

            A.CallTo(() => _orderRepository.OrderExists(orderId)).Returns(true);
            A.CallTo(() => _orderRepository.GetDishesFromOrder(orderId)).Returns(dishes);
            A.CallTo(() => _mapper.Map<List<DishDTO>>(dishes)).Returns(dishesMapped);

            var controller = new OrderController(_orderRepository, _userRepository, _dishRepository, _mapper);

            var result = controller.GetDishesFromOrder(orderId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
