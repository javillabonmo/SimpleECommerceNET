


namespace Application.Services
{
    using Application.DTOs;
    using Application.Services.Interfaces;
    using Application.Services.Mocks;

    using Domain.Entities.Sales;

    using Infraestructure.Persistence;

    public class CategoryService : ICategoryService



    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext applicationDbContext, bool initialize = false)
        {
            _dbContext = applicationDbContext;
        }
        public CategoryResponse AddCategory(CategoryAddRequest? addCategoryRequest)
        {
            if (addCategoryRequest == null)
            {
                throw new ArgumentNullException(nameof(addCategoryRequest), "CategoryAddRequest cannot be null.");
            }
            //convertir la solicitud a una entidad de dominio
            Category category = addCategoryRequest.ToCategory();

            category.CategoryId = Guid.NewGuid(); // Asignar un nuevo GUID

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            // Convertir la entidad de dominio a una respuesta DTO
            return category.ToCategoryResponse();
        }

        public bool DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryResponse? GetCategoryByGuid(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException("Guid cannot be empty.", nameof(guid));
            }
            return _dbContext.Categories.FirstOrDefault(category => category.CategoryId == guid)?.ToCategoryResponse();
        }

        public CategoryResponse? GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryResponse> GetCategories()
        {
            return _dbContext.Categories.Select(c => c.ToCategoryResponse());
        }

        public CategoryResponse? UpdateCategory(int id, CategoryAddRequest? updateCategoryRequest)
        {
            throw new NotImplementedException();
        }
    }

}
