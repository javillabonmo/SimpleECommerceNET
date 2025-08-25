using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class guidsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("14b0e6f5-3414-4d10-adfa-ab97981e34ab"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("230e01d2-3f77-4fdc-b8a6-328804001d93"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "Description", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("adc7f81e-d8ef-4cab-bbb6-32868d5681f4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Administrator role with full access", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Admin" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "ConfirmPassword", "CreatedAt", "CreatedBy", "DeactivatedAt", "Email", "FirstName", "IsActive", "LastName", "LastUpdatedAt", "LastUpdatedBy", "Password", "RoleId", "Username" },
                values: new object[] { new Guid("be04f811-123b-420b-938b-04ba64248f0a"), "Admin1234", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Admin@Example.com", null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Admin1234", new Guid("adc7f81e-d8ef-4cab-bbb6-32868d5681f4"), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("be04f811-123b-420b-938b-04ba64248f0a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("adc7f81e-d8ef-4cab-bbb6-32868d5681f4"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "Description", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("230e01d2-3f77-4fdc-b8a6-328804001d93"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Administrator role with full access", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Admin" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "ConfirmPassword", "CreatedAt", "CreatedBy", "DeactivatedAt", "Email", "FirstName", "IsActive", "LastName", "LastUpdatedAt", "LastUpdatedBy", "Password", "RoleId", "Username" },
                values: new object[] { new Guid("14b0e6f5-3414-4d10-adfa-ab97981e34ab"), "Admin1234", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "Admin@Example.com", null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Admin1234", new Guid("230e01d2-3f77-4fdc-b8a6-328804001d93"), null });
        }
    }
}
