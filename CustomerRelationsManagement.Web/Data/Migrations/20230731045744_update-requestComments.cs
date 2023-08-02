using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class updaterequestComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b750f9ba-98b2-4fba-9808-df36c2f588a4", "AQAAAAIAAYagAAAAEGtIqUUi/JVUmo8o+eO6QRk+kv3YLQGWNlhgQtYISnNbFODzr60qX3c/unAtMbBvHg==", "68f74e82-c2ad-4305-9cb8-e5df823ab7d8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd860be9-1846-497b-ac1e-533f86a1e35a", "AQAAAAIAAYagAAAAEKmozdRofaJB/WrXajyQZR/4ojOTD0qy+dun68+2zw2BJzjGSzpcJdjnKmr1PNVJ+Q==", "68c8f667-6193-4f2b-8dc7-521ed44690b2" });
        }
    }
}
