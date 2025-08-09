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
    public static List<Category> All()
    {
        return new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("7bc4b6da-06fb-4292-8b56-563cc51e25c7"),
                    CategoryName = "Categoria 1",
                    Discount = 10,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                },
                new Category
                {
                    Id = Guid.Parse("85b910fd-7a37-4621-a071-fdd6dd95f9b5"),
                    CategoryName = "Categoria 2",
                    Discount = 15,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                },
                new Category
                {
                    Id = Guid.Parse("f6070eff-7358-43cf-88d0-99dcc8ea1cb1"),
                    CategoryName = "Categoria 3",
                    Discount = 99,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                }
            };
    }

    public static Category GetCategoryByName(string categoryName)
    {
        return All().FirstOrDefault(c => c.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
    }

    #endregion
}

