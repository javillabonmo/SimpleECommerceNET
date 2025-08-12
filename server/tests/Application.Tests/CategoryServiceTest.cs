


namespace Application.Tests
{
    using Application.DTOs;
    using Application.Services;
    using Application.Services.Interfaces;

    using Infraestructure.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class CategoryServiceTest
    {
        private readonly ICategoryService _categoryService;

        public CategoryServiceTest()
        {
            _categoryService = new CategoryService(new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().Options));
        }

        [Fact]
        //1. 
        public void CategoryService_AddCategory_ShouldReturnCategoryResponse()
        {
            // Arrange

            CategoryAddRequest requestCategory = new CategoryAddRequest { CategoryName = "Electronics" };

            // Act
            CategoryResponse category = _categoryService.AddCategory(requestCategory);
            IEnumerable<CategoryResponse> categories = _categoryService.GetCategories();
            // Assert
            Assert.NotNull(category);
            Assert.True(category.CategoryId != Guid.Empty, "Category ID should not be empty.");
            Assert.Contains(category, categories);
        }
    }
}

