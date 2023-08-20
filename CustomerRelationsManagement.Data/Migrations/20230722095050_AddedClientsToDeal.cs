using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedClientsToDeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3c92c68-5d84-460c-a03c-1788708a1f34", "AQAAAAIAAYagAAAAEENAHjCa6CwBBLeOrM1toSPjhAsRUbVK1j4EaU/vqNVAZzrJ2/F9tefm5cCtXm4Hkg==", "c546f1be-fcd9-4858-9f14-d9d1250f53db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f99b4ae7-09b9-4ac4-b810-537b7145866a", "AQAAAAIAAYagAAAAEG/lXrNvnJyafUW76DRI+TK4i2vCac0gIM9P4bs6OCpHomMW9Pujlsc531wDiVkDNQ==", "62e20635-e083-4a82-9ede-049c04c9076e" });
        }
    }
}
