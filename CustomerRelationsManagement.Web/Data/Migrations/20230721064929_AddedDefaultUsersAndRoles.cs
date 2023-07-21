using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87d97513-99b0-4df4-b06e-fe694e99c433", null, "User", "USER" },
                    { "87d97800-99b0-4df4-b06e-fe694e99c433", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "87d97800-99c0-4df4-b06e-fe094e99c433", 0, "aa69b589-a478-49e6-8cb7-53d0571330f6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@email.com", false, "System", "Admin", false, null, "ADMIN@EMAIL.COM", null, "AQAAAAIAAYagAAAAEAbyVIPX8/4AAF6wsJhJcGsFmKIRmk/E2XdqUktrcljaFquVJ9awoNziCDLJJKfBmg==", null, false, "acd1265e-d295-4af1-9ebd-fe3bfa905a14", null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b06e-fe094e99c433" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87d97513-99b0-4df4-b06e-fe694e99c433");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b06e-fe094e99c433" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87d97800-99b0-4df4-b06e-fe694e99c433");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c433");
        }
    }
}
