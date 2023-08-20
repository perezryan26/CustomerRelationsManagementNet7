using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultUsersAndRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b06e-fe094e99c433" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c433");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "87d97800-99c0-4df4-b06e-fe094e99c111", 0, "e438887f-2873-4c8f-bcea-bb51d8a570e8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@email.com", true, "System", "Admin", false, null, "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEIM2dcnk6S2R9fy9wpOqToprHz9QZVtQk2qB8xM6b1j5Eq+NQgbBv8LoFj6E/Cq+/Q==", null, false, "ab2788e5-53ba-45d7-ac57-bdf69fa90496", null, false, "admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b06e-fe094e99c111" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "87d97800-99c0-4df4-b06e-fe094e99c433", 0, "aa69b589-a478-49e6-8cb7-53d0571330f6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@email.com", false, "System", "Admin", false, null, "ADMIN@EMAIL.COM", null, "AQAAAAIAAYagAAAAEAbyVIPX8/4AAF6wsJhJcGsFmKIRmk/E2XdqUktrcljaFquVJ9awoNziCDLJJKfBmg==", null, false, "acd1265e-d295-4af1-9ebd-fe3bfa905a14", null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b06e-fe094e99c433" });
        }
    }
}
