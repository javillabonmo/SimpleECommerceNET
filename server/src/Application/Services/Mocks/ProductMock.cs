




namespace Application.Services.Mocks
{
    using Domain.Entities.Inventory;
    public static class ProductMock
    {
        #region productsMock



        public static List<Product> All()
        {
            return new List<Product>
            {
                new Product
                {
                    ProductId = Guid.Parse("581ff516-be77-4907-afe2-e323d84d08d9"),
                    ProductName = "Producto 1",
                    Price = 50.00m,
                    Stock = 100,
                    Category = CategoryMock.GetCategoryByName("Categoria 1"),
                    CategoryId = CategoryMock.GetCategoryByName("Categoria 1").CategoryId,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                },
                new Product
                {
                    ProductId =Guid.Parse("d4fbcc41-2ac8-4535-9222-6acee1d4e4a8"),
                    ProductName = "Producto 2",
                    Price = 75.00m,
                    Stock = 200,
                    Category =CategoryMock.GetCategoryByName("Categoria 2"),
                    CategoryId = CategoryMock.GetCategoryByName("Categoria 2").CategoryId,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                },
                new Product
                {
                    ProductId =Guid.Parse("5ccbba0e-b2b5-4614-a947-85fb4dd375f5"),
                    ProductName = "Producto 3",
                    Price = 75.00m,
                    Stock = 200,
                    Category = CategoryMock.GetCategoryByName("Categoria 3"),
                    CategoryId = CategoryMock.GetCategoryByName("Categoria 3").CategoryId,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                }
            };
        }



        #endregion
    }
}


