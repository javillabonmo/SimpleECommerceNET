// <copyright file="UsersDbContext.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

namespace Infraestructure.Persistence
{
    using Domain.Entities.AuthDB;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the <see cref="UsersDbContext" />
    /// </summary>
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions options)
            : base(options)
        {
        }
        /// <summary>
        /// Gets or sets the Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// The OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("Usuarios");
            modelBuilder.Entity<Role>().ToTable("Roles");

            // seeds
            var adminRoleId = Guid.NewGuid();

            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = adminRoleId,
                Name = "Admin",
                Description = "Administrator role with full access",
            });
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Email = "Admin@Example.com",
                Password = "Admin1234",
                ConfirmPassword = "Admin1234",
                IsActive = true,
                RoleId = adminRoleId,
            }

            );
        }
    }
}
