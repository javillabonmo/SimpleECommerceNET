// <copyright file="Category.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

namespace SimpleEcommerce.Core.Domain.Entities.Sales
{
    using System.ComponentModel.DataAnnotations;

    using SimpleEcommerce.Core.Domain.Entities.Common;
    using SimpleEcommerce.Core.Domain.Entities.Inventory;

    /// <summary>
    /// Defines the <see cref="Category" />
    /// </summary>
    public class Category : AuditableEntityBase
    {
        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        [Key]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryName
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the Discount
        /// </summary>
        public int Discount { get; set; }

        public int InternalId { get; set; }

        public virtual ICollection<Product>? Products { get; set; }//por cada Id de categoria, una coleccion de productos asociados a la categoria 1:n
        //public virtual Product? Product { get; set; } //por cada Id de categoria, un producto asociado a la categoria 1:1
    }
}
