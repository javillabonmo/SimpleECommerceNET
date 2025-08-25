


namespace SimpleECommerce.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SimpleEcommerce.Core.Domain.Entities.Inventory;
    using SimpleEcommerce.Core.Domain.Entities.Sales;
    using SimpleEcommerce.Core.Domain.RespositoryContracts;
    using SimpleEcommerce.Core.DTOs;

    using SimpleECommerce.Core.DTOs;
    using SimpleECommerce.Core.Helpers;
    using SimpleECommerce.Core.ServicesContrats;
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }



        public async Task<CategoryResponse> AddCategory(CategoryRequest? addCategoryRequest)
        {
            ValidationHelper.Validate(addCategoryRequest);

            Category category = addCategoryRequest.ToCategory();
            category.CategoryId = Guid.NewGuid();

            await _categoryRepository.AddCategoryAsync(category);
            return category.ToCategoryResponse();
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            Category? category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return false; // Product not found, nothing to delete
            }
            await _categoryRepository.DeleteCategoryAsync(id);
            return true;
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            List<Category> categories = await _categoryRepository.GetCategoriesAsync();
            return categories.Select(c => c.ToCategoryResponse());
        }

        public async Task<CategoryResponse?> GetCategoryByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponse?> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponse?> UpdateCategory(int id, CategoryRequest? updateCategoryRequest)
        {
            throw new NotImplementedException();
        }
    }

}
