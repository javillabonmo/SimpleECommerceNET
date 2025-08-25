

namespace SimpleEcommerce.Core.DTOs
{
    using System.ComponentModel.DataAnnotations;

    using SimpleEcommerce.Core.Domain.Entities.Common;
    using SimpleEcommerce.Core.Domain.Entities.Sales;

    public class CategoryRequest
    {
        public string CategoryName { get; set; }
        public int Discount { get; set; }
        public Category ToCategory()
        {
            return new Category
            {
                CategoryName = CategoryName,
                Discount = Discount
            };
        }

    }

    public class CategoryResponse : IAuditableEntityBase
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int Discount { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public Guid LastUpdatedBy { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is not CategoryResponse responseCategory)
            {
                return false;
            }

            return CategoryId == responseCategory.CategoryId &&
                   CategoryName == responseCategory.CategoryName &&
                   Discount == responseCategory.Discount &&
                     CreatedAt == responseCategory.CreatedAt &&
                     CreatedBy == responseCategory.CreatedBy &&
                        LastUpdatedAt == responseCategory.LastUpdatedAt &&
                        LastUpdatedBy == responseCategory.LastUpdatedBy;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"ProductId: {CategoryId}, ProductName: {CategoryName}, Discount: {Discount}, CreatedAt: {CreatedAt}, LastUpdatedAt: {LastUpdatedAt}";
        }

        public CategoryUpdateRequest ToCategoryUpdateRequest()
        {
            return new CategoryUpdateRequest
            {
                CategoryId = CategoryId,
                CategoryName = CategoryName,
                Discount = Discount
            };
        }
    }

    public class CategoryUpdateRequest : CategoryRequest
    {
        [Required(ErrorMessage = "CategoryId is required")]
        public Guid CategoryId { get; set; }
    }

    public static class CategoryExtensions
    {
        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Discount = category.Discount,
                CreatedAt = category.CreatedAt,
                CreatedBy = category.CreatedBy,
                LastUpdatedAt = category.LastUpdatedAt,
                LastUpdatedBy = category.LastUpdatedBy
            };
        }
    }
}