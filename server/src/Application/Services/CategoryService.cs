


namespace Application.Services
{
    using Application.DTOs;
    using Application.Services.Interfaces;
    using Application.Services.Mocks;

    using Domain.Entities.Sales;

    public class CategoryService : ICategoryService



    {
        private readonly List<Category> _categories;
        public CategoryService(bool initialize = true)
        {
            // Simulando una base de datos en memoria
            // Aquí podrías inicializar una lista o un contexto de base de datos
            _categories = new List<Category>();
            if (initialize)
            {
                foreach (Category category in CategoryMock.All())
                {
                    // Convertir ProductResponse a Product y agregarlo a la lista
                    _categories.Add(category);
                }
            }
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

            _categories.Add(category);

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
            return _categories.FirstOrDefault(category => category.CategoryId == guid)?.ToCategoryResponse();
        }

        public CategoryResponse? GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryResponse> GetCategories()
        {
            return _categories.Select(c => c.ToCategoryResponse());
        }

        public CategoryResponse? UpdateCategory(int id, CategoryAddRequest? updateCategoryRequest)
        {
            throw new NotImplementedException();
        }
    }

}
