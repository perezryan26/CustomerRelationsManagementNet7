using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class newAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b06e-fe094e99c111" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Department", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "87d97800-99c0-4df4-b011-fe094e99c111", 0, "9fd8b4b5-bda0-4f49-ba90-59c2ec4f2e3e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@admin.com", true, "System", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEAzz2jPelXYcw7abvZtJ7vZxIn9pgApoAAcZW0urgQT7IdZ1nTEn9hF13W33ViT6Sw==", null, false, 1, "63985d43-e1bf-4813-b332-014550024a8b", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b011-fe094e99c111" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b011-fe094e99c111" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b011-fe094e99c111");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Department", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "87d97800-99c0-4df4-b06e-fe094e99c111", 0, "569759dd-4d2f-4bb2-a360-cc092b6e09f9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@admin.com", true, "System", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEJPyQooT44de6si4HQe0lS8Fwv+hZrqcNOUBYIzPumqS0QA8C8vxrkee8PPZtSZChQ==", null, false, 1, "e9c8f3e7-32c4-43d4-b951-8f5f2836b339", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b06e-fe094e99c111" });
        }
    }
}
