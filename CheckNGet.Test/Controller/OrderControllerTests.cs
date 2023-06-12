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
        [Fact]
        public void OrderController_CreateOrder_ReturnOk()
        {
            var order = A.Fake<Order>();
            var user = A.Fake<User>();
            var userId = user.Id;
            var dish = A.Fake<Dish>();
            var dishId = dish.Id;
            var orderCreate = A.Fake<OrderDTO>();

            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _dishRepository.DishExists(dishId)).Returns(true);
            A.CallTo(() => _orderRepository.CompareOrders(orderCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Order>(orderCreate)).Returns(order);
            A.CallTo(() => _userRepository.GetUser(userId)).Returns(user);
            A.CallTo(() => _orderRepository.CreateOrder(dishId, order)).Returns(true);

            var controller = new OrderController(_orderRepository, _userRepository, _dishRepository, _mapper);

            var result = controller.CreateOrder(userId, dishId, orderCreate);

            A.CallTo(() => _userRepository.UserExists(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _dishRepository.DishExists(dishId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepository.CompareOrders(orderCreate)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _mapper.Map<Order>(orderCreate)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.GetUser(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepository.CreateOrder(dishId, order)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Successfully created!");
        }
        [Fact]
        public void OrderController_UpdateOrder_ReturnNoContent()
        {
            var order = A.Fake<Order>();
            var orderId = order.Id;
            var dish = A.Fake<Dish>();
            var dishId = dish.Id;
            var orderUpdate = A.Fake<OrderDTO>();
            var orderMapped = A.Fake<Order>();

            A.CallTo(() => _orderRepository.OrderExists(orderId)).Returns(true);
            A.CallTo(() => _mapper.Map<Order>(orderUpdate)).Returns(orderMapped);
            A.CallTo(() => _orderRepository.UpdateOrder(dishId, orderMapped)).Returns(true);

            var controller = new OrderController(_orderRepository, _userRepository, _dishRepository, _mapper);

            var result = controller.UpdateOrder(orderId, dishId, orderUpdate);

            A.CallTo(() => _orderRepository.UpdateOrder(dishId, orderMapped)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
        [Fact]
        public void OrderController_DeleteOrder_ReturnNoContent()
        {
            var order = A.Fake<Order>();
            var orderId = order.Id;

            A.CallTo(() => _orderRepository.OrderExists(orderId)).Returns(true);
            A.CallTo(() => _orderRepository.GetOrder(orderId)).Returns(order);
            A.CallTo(() => _orderRepository.DeleteOrder(order)).Returns(true);

            var controller = new OrderController(_orderRepository, _userRepository, _dishRepository, _mapper);

            var result = controller.DeleteOrder(orderId);

            A.CallTo(() => _orderRepository.GetOrder(orderId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _orderRepository.DeleteOrder(order)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
