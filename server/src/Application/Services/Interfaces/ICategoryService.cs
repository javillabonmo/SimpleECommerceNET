

namespace Application.Services.Interfaces
{

    using Application.DTOs;

    public interface ICategoryService
    {
        Task<CategoryResponse> AddCategory(CategoryAddRequest? addCategoryRequest);

        Task<CategoryResponse?> GetCategoryById(int id);

        Task<CategoryResponse?> GetCategoryByGuid(Guid guid);

        Task<IEnumerable<CategoryResponse>> GetCategories();

        Task<CategoryResponse?> UpdateCategory(int id, CategoryAddRequest? updateCategoryRequest);

        Task<bool> DeleteCategory(int id);
    }

}
