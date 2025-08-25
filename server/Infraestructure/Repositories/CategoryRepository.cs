


using Microsoft.EntityFrameworkCore;

using SimpleEcommerce.Core.Domain.Entities.Inventory;
using SimpleEcommerce.Core.Domain.Entities.Sales;
using SimpleEcommerce.Core.Domain.RespositoryContracts;

using SimpleECommerce.Infraestructure.Persistence;

namespace Infraestructure.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Category> AddCategoryAsync(Category? category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            _dbContext.RemoveRange(_dbContext.Categories.Where(p => p.CategoryId == id));
            int deletedRows = await _dbContext.SaveChangesAsync();
            return deletedRows > 0;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            Category? category = await _dbContext.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(Category? category)
        {
            Category? existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(p => p.CategoryId == category.CategoryId);
            if (existingCategory == null) return null;

            existingCategory.CategoryName = category.CategoryName ?? existingCategory.CategoryName;

            existingCategory.Discount = category.Discount != 0 ? category.Discount : existingCategory.Discount;

            await _dbContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}

