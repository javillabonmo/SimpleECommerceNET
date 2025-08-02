using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services.Interfaces;
public interface ICategoryService
{
    CategoryResponse AddCategory(RequestCategory? addCategoryRequest);

    CategoryResponse? GetCategoryById(int id);

    CategoryResponse? GetCategoryByGuid(Guid guid);
    IEnumerable<CategoryResponse> GetCategories();
    CategoryResponse? UpdateCategory(int id, RequestCategory? updateCategoryRequest); 
    bool DeleteCategory(int id);
}
