using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Application.Services;
using Application.DTOs;

namespace Application.Tests;
public class CategoryServiceTest
{
    private readonly ICategoryService _categoryService;

    public CategoryServiceTest()
    {
        _categoryService = new CategoryService();
    }

    [Fact] 
    //1. 
    public void CategoryService_AddCategory_ShouldReturnCategoryResponse()
    {
        // Arrange
      
        RequestCategory requestCategory = new RequestCategory { Name = "Electronics" };
        
        // Act
        CategoryResponse category = _categoryService.AddCategory(requestCategory);
        IEnumerable<CategoryResponse> categories = _categoryService.GetCategories();
        // Assert
        Assert.NotNull(category);
        Assert.True(category.Id != Guid.Empty, "Category ID should not be empty.");
        Assert.Contains(category, categories);
    }
}
