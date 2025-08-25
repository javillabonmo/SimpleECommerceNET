// <copyright file="Product.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

namespace SimpleEcommerce.Core.Domain.Entities.Inventory
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SimpleEcommerce.Core.Domain.Entities.Common;
    using SimpleEcommerce.Core.Domain.Entities.Sales;

    /// <summary>
    /// Defines the <see cref="Product" />
    /// </summary>
    public class Product : AuditableEntityBase
    {
        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        [Key]
        public Guid ProductId { get; set; }

        public int InternalId { get; set; }

        /// <summary>
        /// Gets or sets the ProductName
        /// </summary>
        [StringLength(100)]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the Category
        /// </summary>


        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the Stock
        /// </summary>
        public uint Stock { get; set; }

        /// <summary>
        /// Gets or sets the Price
        /// </summary>
        public required decimal Price { get; set; }
    }
}
