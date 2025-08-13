// <copyright file="UsersDbContext.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

namespace Infraestructure.Persistence
{
    using Domain.Entities.AuthDB;
    using Domain.Entities.Inventory;
    using Domain.Entities.Sales;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the <see cref="ApplicationDbContext" />
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
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

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// The OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // FluentAPI
            modelBuilder.Entity<User>().Property(p => p.IsActive).HasDefaultValue("true");
            modelBuilder.Entity<User>().ToTable("Usuarios");

            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Category>()
                .ToTable("Categorias");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnName("Precio").HasColumnType("decimal(18,4)");
            modelBuilder.Entity<Product>().Property(p => p.InternalId).ValueGeneratedOnAdd(); // id autoincremental
            modelBuilder.Entity<Product>().HasIndex(p => p.InternalId).IsUnique(); // indice unico
            modelBuilder.Entity<Product>()
                .ToTable("Productos", p => p.HasCheckConstraint("CK_PrecioStock_Positivos", "[Precio] > 0 AND [Stock] > 0"));

            // seeds
            var adminRoleId = Guid.Parse("adc7f81e-d8ef-4cab-bbb6-32868d5681f4");
            var productRoleId = Guid.Parse("be04f811-123b-420b-938b-04ba64248f0a");
            var categoryId = Guid.Parse("ca164733-6a40-43c0-8dfd-3a77c0be5924");

            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = adminRoleId,
                Name = "Admin",
                Description = "Administrator role with full access",
            });
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = productRoleId,
                Email = "Admin@Example.com",
                Password = "Admin1234",
                ConfirmPassword = "Admin1234",

                RoleId = adminRoleId,
            }

            );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = categoryId,
                    CategoryName = "seeded category",
                });
        }
    }
}
