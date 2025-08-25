



namespace SimpleECommerce.Core.ServicesContrats
{

    using SimpleEcommerce.Core.DTOs;

    public interface ICategoryService
    {
        Task<CategoryResponse> AddCategory(CategoryRequest? addCategoryRequest);

        Task<CategoryResponse?> GetCategoryById(int id);

        Task<CategoryResponse?> GetCategoryByGuid(Guid guid);

        Task<IEnumerable<CategoryResponse>> GetCategories();

        Task<CategoryResponse?> UpdateCategory(int id, CategoryRequest? updateCategoryRequest);

        Task<bool> DeleteCategory(Guid id);
    }

}
