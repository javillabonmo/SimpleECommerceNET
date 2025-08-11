

namespace Application.Services.Interfaces
{

    using Application.DTOs;
    public interface ICategoryService
    {
        CategoryResponse AddCategory(CategoryAddRequest? addCategoryRequest);

        CategoryResponse? GetCategoryById(int id);

        CategoryResponse? GetCategoryByGuid(Guid guid);
        IEnumerable<CategoryResponse> GetCategories();
        CategoryResponse? UpdateCategory(int id, CategoryAddRequest? updateCategoryRequest);
        bool DeleteCategory(int id);
    }

}
