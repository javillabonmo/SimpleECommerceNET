using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Entities.Sales;

namespace Application.Services;
public class CategoryService : ICategoryService



{
    private readonly List<Category> _categories;
    public CategoryService()
    {
        // Simulando una base de datos en memoria
        // Aquí podrías inicializar una lista o un contexto de base de datos
        _categories = new List<Category>();
    }
    public CategoryResponse AddCategory(RequestCategory? addCategoryRequest)
    {
        if(addCategoryRequest == null)
        {
            throw new ArgumentNullException(nameof(addCategoryRequest), "RequestCategory cannot be null.");
        }
        //convertir la solicitud a una entidad de dominio
        Category category = addCategoryRequest.ToCategory();

        category.Id = Guid.NewGuid(); // Asignar un nuevo GUID

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
        throw new NotImplementedException();
    }

    public CategoryResponse? GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CategoryResponse> GetCategories()
    {
        return _categories.Select(c => c.ToCategoryResponse());
    }

    public CategoryResponse? UpdateCategory(int id, RequestCategory? updateCategoryRequest)
    {
        throw new NotImplementedException();
    }
}
