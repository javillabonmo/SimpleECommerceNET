


namespace Application.Services
{
    using Application.DTOs;
    using Application.Services.Interfaces;


    using Domain.Entities.Sales;

    using Infraestructure.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext applicationDbContext, bool initialize = false)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<CategoryResponse> AddCategory(CategoryAddRequest? addCategoryRequest)
        {
            if (addCategoryRequest == null)
            {
                throw new ArgumentNullException(nameof(addCategoryRequest), "CategoryAddRequest cannot be null.");
            }
            //convertir la solicitud a una entidad de dominio
            Category category = addCategoryRequest.ToCategory();

            category.CategoryId = Guid.NewGuid(); // Asignar un nuevo GUID

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            // Convertir la entidad de dominio a una respuesta DTO
            return category.ToCategoryResponse();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryResponse?> GetCategoryByGuid(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException("Guid cannot be empty.", nameof(guid));
            }
            Category? category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == guid);

            return category?.ToCategoryResponse();
        }

        public async Task<CategoryResponse?> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            List<Category> categories = await _dbContext.Categories.ToListAsync();
            return categories.Select(c => c.ToCategoryResponse());
        }

        public async Task<CategoryResponse?> UpdateCategory(int id, CategoryAddRequest? updateCategoryRequest)
        {
            throw new NotImplementedException();
        }
    }

}
