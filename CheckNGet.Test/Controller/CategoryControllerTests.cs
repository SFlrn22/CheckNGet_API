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
    public class CategoryControllerTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        public CategoryControllerTests()
        {
            _categoryRepository = A.Fake<ICategoryRepository>();
            _dishRepository = A.Fake<IDishRepository>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void CategoryController_GetCategories_ReturnOK()
        {
            var categories = A.Fake<List<Category>>();
            var categoryList = A.Fake<List<CategoryDTO>>();
            
            A.CallTo(() => _categoryRepository.GetCategories()).Returns(categories);
            A.CallTo(() => _mapper.Map<List<CategoryDTO>>(categories)).Returns(categoryList);

            var controller = new CategoryController(_categoryRepository, _dishRepository, _mapper);

            var result = controller.GetCategories();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void CategoryController_GetCategory_ReturnOK()
        {
            var category = A.Fake<Category>();
            var categoryId = category.Id;
            var categoryMapped = A.Fake<CategoryDTO>();

            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _categoryRepository.GetCategory(categoryId)).Returns(category);
            A.CallTo(() => _mapper.Map<CategoryDTO>(category)).Returns(categoryMapped);

            var controller = new CategoryController(_categoryRepository, _dishRepository, _mapper);

            var result = controller.GetCategory(categoryId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void CategoryController_GetDishByCategoryId_ReturnOK()
        {
            var category = A.Fake<Category>();
            var categoryId = category.Id;
            var dishList = A.Fake<List<Dish>>();
            var dishListMapped = A.Fake<List<DishDTO>>();

            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _categoryRepository.GetDishByCategory(categoryId)).Returns(dishList);
            A.CallTo(() => _mapper.Map<List<DishDTO>>(dishList)).Returns(dishListMapped);

            var controller = new CategoryController(_categoryRepository, _dishRepository, _mapper);

            var result = controller.GetDishByCategoryId(categoryId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
