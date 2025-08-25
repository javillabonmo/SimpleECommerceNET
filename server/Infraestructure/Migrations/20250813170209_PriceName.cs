using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class PriceName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Productos",
                newName: "Precio");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Productos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "InternalId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "InternalId",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoryId", "CategoryName", "CreatedAt", "CreatedBy", "Discount", "InternalId", "LastUpdatedAt", "LastUpdatedBy" },
                values: new object[] { new Guid("ca164733-6a40-43c0-8dfd-3a77c0be5924"), "seeded category", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_InternalId",
                table: "Productos",
                column: "InternalId",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_PrecioStock_Positivos",
                table: "Productos",
                sql: "[Precio] > 0 AND [Stock] > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_InternalId",
                table: "Productos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PrecioStock_Positivos",
                table: "Productos");

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoryId",
                keyValue: new Guid("ca164733-6a40-43c0-8dfd-3a77c0be5924"));

            migrationBuilder.DropColumn(
                name: "InternalId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "InternalId",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Productos",
                newName: "Price");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
