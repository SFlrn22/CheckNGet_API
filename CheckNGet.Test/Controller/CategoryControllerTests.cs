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
        [Fact]
        public void CategoryController_CreateCategory_ReturnOK()
        {
            var categoryCreate = A.Fake<CategoryDTO>();
            var category = A.Fake<Category>();

            A.CallTo(() => _categoryRepository.CompareCategories(categoryCreate)).Returns(null);
            A.CallTo(() => _mapper.Map<Category>(categoryCreate)).Returns(category);
            A.CallTo(() => _categoryRepository.CreateCategory(category)).Returns(true);

            var controller = new CategoryController(_categoryRepository, _dishRepository, _mapper);

            var result = controller.CreateCategory(categoryCreate);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Successfully created!");
        }
        [Fact]
        public void CategoryController_UpdateCategory_ReturnNoContent()
        {
            var updateCategory = A.Fake<CategoryDTO>();
            var category = A.Fake<Category>();
            var categoryId = category.Id;

            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _mapper.Map<Category>(updateCategory)).Returns(category);
            A.CallTo(() => _categoryRepository.UpdateCategory(category)).Returns(true);

            var controller = new CategoryController(_categoryRepository, _dishRepository, _mapper);

            var result = controller.UpdateCategory(categoryId, updateCategory);

            A.CallTo(() => _categoryRepository.UpdateCategory(category)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
        [Fact]
        public void CategoryController_DeleteCategory_ReturnNoContent()
        {
            var category = A.Fake<Category>();
            var categoryId = category.Id;
            var dishesToDelete = A.Fake<List<Dish>>();

            A.CallTo(() => _categoryRepository.CategoryExists(categoryId)).Returns(true);
            A.CallTo(() => _categoryRepository.GetDishByCategory(categoryId)).Returns(dishesToDelete);
            A.CallTo(() => _categoryRepository.GetCategory(categoryId)).Returns(category);
            A.CallTo(() => _dishRepository.DeleteDishes(A<List<Dish>>.That.Matches(d => d.SequenceEqual(dishesToDelete)))).Returns(true);
            A.CallTo(() => _categoryRepository.DeleteCategory(category)).Returns(true);

            var controller = new CategoryController(_categoryRepository, _dishRepository, _mapper);

            var result = controller.DeleteCategory(categoryId);

            A.CallTo(() => _categoryRepository.GetDishByCategory(categoryId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _categoryRepository.GetCategory(categoryId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _dishRepository.DeleteDishes(A<List<Dish>>.That.Matches(d => d.SequenceEqual(dishesToDelete)))).MustHaveHappenedOnceExactly();
            A.CallTo(() => _categoryRepository.DeleteCategory(category)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
