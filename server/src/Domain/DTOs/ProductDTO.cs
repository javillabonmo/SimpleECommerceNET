namespace SimpleECommerce.Core.DTOs
{
    using System.ComponentModel.DataAnnotations;

    using SimpleEcommerce.Core.Domain.Entities.Common;
    using SimpleEcommerce.Core.Domain.Entities.Inventory;
    using SimpleEcommerce.Core.Domain.Entities.Sales;

    public class ProductRequest
    {

        [DataType(DataType.Text)]
        public string ProductName { get; set; } = string.Empty;

        public Category? Category { get; set; }

        public Guid CategoryId { get; set; }

        public uint Stock { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public Product ToProduct()
        {
            return new Product() { ProductName = ProductName, Price = Price, Stock = Stock, Category = Category, CategoryId = CategoryId };

        }
    }

    public class ProductResponse : IAuditableEntityBase
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public uint Stock { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public Guid LastUpdatedBy { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not ProductResponse productResponse)
            {
                return false;
            }
            return ProductId == productResponse.ProductId &&
                ProductName == productResponse.ProductName &&

                   Category.Equals(productResponse.Category) &&
                    CategoryId == productResponse.CategoryId &&
                   Stock == productResponse.Stock &&
                   Price == productResponse.Price &&
            CreatedAt == productResponse.CreatedAt &&
                   CreatedBy == productResponse.CreatedBy &&
                   LastUpdatedAt == productResponse.LastUpdatedAt &&
                   LastUpdatedBy == productResponse.LastUpdatedBy;

            //return base.Equals(obj);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"ProductResponse(ProductId: {ProductId}, ProductName: {ProductName}, Price: {Price}, Stock: {Stock}, Category: {Category?.CategoryName})";
        }

        public ProductUpdateRequest ToProductUpdateRequest()
        {
            return new ProductUpdateRequest()
            {
                ProductId = ProductId,
                ProductName = ProductName,
                Category = Category,
                CategoryId = CategoryId,
                Stock = Stock,
                Price = Price
            };
        }


    }



    public class ProductUpdateRequest : ProductRequest
    {
        [Required(ErrorMessage = "ProductId is required")]
        public Guid ProductId { get; set; }
    }

    public static class ProductExtensions
    {
        public static ProductResponse ToProductResponse(this Product product)//convierte el product a productResponse
        {
            return new ProductResponse()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CreatedAt = product.CreatedAt,
                CreatedBy = product.CreatedBy,
                LastUpdatedAt = product.LastUpdatedAt,
                LastUpdatedBy = product.LastUpdatedBy,
                Category = product.Category,
                CategoryId = product.CategoryId,
                Stock = product.Stock,
                Price = product.Price
            };
        }
    }
}
