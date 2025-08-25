
using SimpleEcommerce.Core.Domain.Entities.Sales;

namespace SimpleEcommerce.Core.Domain.RespositoryContracts
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category? category);

        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<List<Category>> GetCategoriesAsync();

        Task<Category?> UpdateCategoryAsync(Category? category);

        Task<bool> DeleteCategoryAsync(Guid id);
    }

}
