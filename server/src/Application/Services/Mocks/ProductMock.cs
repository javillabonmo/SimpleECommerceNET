using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services.Helpers;

namespace Application.Services.Mocks;
public static class ProductMock
{
    #region productsMock

    public static ProductRequest ProductAddRequest()
    {
        return new ProductRequest
        {
            Name = "Producto Nuevo",
            Price = 99.99m,
            Stock = 10,
            CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };
    }

    public static ProductUpdateRequest ProductUpdateRequest(Guid productId)
    {
        return new ProductUpdateRequest
        {
            Id = productId,
            Name = "Producto Actualizado",
            Price = 149.99m,
            Stock = 20,
            CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222")
        };
    }

    public static List<ProductResponse> All()
    {
        return new List<ProductResponse>
            {
                new ProductResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Producto 1",
                    Price = 50.00m,
                    Stock = 100,
                    Category = new CategoryResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "Categoria 1",
                        Discount = 10,
                        CreatedAt = DateTime.Now,
                        LastUpdatedAt = DateTime.Now
                    },
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                },
                new ProductResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Producto 2",
                    Price = 75.00m,
                    Stock = 200,
                    Category = new CategoryResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "Categoria 2",
                        Discount = 15,
                        CreatedAt = DateTime.Now,
                        LastUpdatedAt = DateTime.Now
                    },
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                },
                new ProductResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "nombre camel2",
                    Price = 75.00m,
                    Stock = 200,
                    Category = new CategoryResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "rara",
                        Discount = 15,
                        CreatedAt = DateTime.Now,
                        LastUpdatedAt = DateTime.Now
                    },
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                }
            };
    }

    public static List<ProductResponse> ProductsFromJson()
    {
        string products = """
            [
                {
                    "Id": "11111111-1111-1111-1111-111111111111",
                    "Name": "Producto 1",
                    "Price": 50.00,
                    "Stock": 100,
                    "Category": {
                        "Id": "22222222-2222-2222-2222-222222222222",
                        "Name": "Categoria 1",
                        "Discount": 10,
                        "CreatedAt": "2023-10-01T00:00:00Z",
                        "LastUpdatedAt": "2023-10-01T00:00:00Z"
                    },
                    "CreatedAt": "2023-10-01T00:00:00Z",
                    "LastUpdatedAt": "2023-10-01T00:00:00Z"
                }
            ]
            """;

        List<ProductResponse>? JsonMock = Deserialize.Json<ProductResponse>(products);
        if (JsonMock is null)
        {
            return new List<ProductResponse>();
        }
        else
        {
            return JsonMock;
        }
    }
    #endregion
}

