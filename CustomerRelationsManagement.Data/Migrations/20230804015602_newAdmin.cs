using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class newAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PositionId", "SecurityStamp" },
                values: new object[] { "569759dd-4d2f-4bb2-a360-cc092b6e09f9", "AQAAAAIAAYagAAAAEJPyQooT44de6si4HQe0lS8Fwv+hZrqcNOUBYIzPumqS0QA8C8vxrkee8PPZtSZChQ==", 1, "e9c8f3e7-32c4-43d4-b951-8f5f2836b339" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PositionId", "SecurityStamp" },
                values: new object[] { "2dd9a624-6960-4e99-b41d-a4c0ae2c4732", "AQAAAAIAAYagAAAAEA8+D+uWQrY9tt1pvB8Yi+EGVw2C9dh1eZwe8CxnQW2tIurfS85iy98h9iVEfiksDg==", 0, "b011d28b-72a0-4702-8e13-770d0169e051" });
        }
    }
}
