// <copyright file="AuditableEntityBase.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

namespace SimpleEcommerce.Core.Domain.Entities.Common
{


    /// <summary>
    /// Defines the <see cref="AuditableEntityBase" />
    /// </summary>
    public abstract class AuditableEntityBase
    {
        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdatedAt.
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdatedBy.
        /// </summary>
        public Guid LastUpdatedBy { get; set; }
    }
}
