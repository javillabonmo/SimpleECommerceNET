
using Application.DTOs;

namespace Application.Services.Interfaces;
public interface ICategoryService
{
    CategoryResponse AddCategory(CategoryAddRequest? addCategoryRequest);

    CategoryResponse? GetCategoryById(int id);

    CategoryResponse? GetCategoryByGuid(Guid guid);
    IEnumerable<CategoryResponse> GetCategories();
    CategoryResponse? UpdateCategory(int id, CategoryAddRequest? updateCategoryRequest); 
    bool DeleteCategory(int id);
}
