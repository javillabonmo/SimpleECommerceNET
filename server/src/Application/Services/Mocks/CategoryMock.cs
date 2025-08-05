using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

using Domain.Entities.Sales;

namespace Application.Services.Mocks;
public static class CategoryMock
{
    #region CategoriesMock
    public static List<CategoryResponse> Categories()
    {
        return new List<CategoryResponse>
            {
                new CategoryResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Categoria 1",
                    Discount = 10,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                },
                new CategoryResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Categoria 2",
                    Discount = 15,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                }
            };
    }

    public static CategoryUpdateRequest CategoryUpdateRequest(Guid categoryId)
    {
        return new CategoryUpdateRequest
        {
            Id = categoryId,
            Name = "Juguetes Actualizado",
            Discount = 20
        };
    }

    public static CategoryAddRequest CategoryAddRequest()
    {
        return new CategoryAddRequest
        {
            Name = "Juguetes",
            Discount = 15
        };
    }

    public static List<Category> CategoriesFromJson()
    {
        return new List<Category>();
    }
    #endregion
}

