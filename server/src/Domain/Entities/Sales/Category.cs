// <copyright file="Category.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

namespace Domain.Entities.Sales
{
    using System.ComponentModel.DataAnnotations;

    using Domain.Entities.Common;

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
    }
}
