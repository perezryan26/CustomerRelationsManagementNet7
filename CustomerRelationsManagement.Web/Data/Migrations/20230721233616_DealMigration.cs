using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class DealMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b40a98a-1618-4ed2-a38b-23c43f1b31e3", "AQAAAAIAAYagAAAAEElzvyqLqIKHL+qBlPbHOeI/hfqV8xNvckiDjMbxpKpxBoG5vsVkjFuzDFcrAtHQSQ==", "461adeac-90aa-4bda-9f72-2c6507e4520e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e438887f-2873-4c8f-bcea-bb51d8a570e8", "AQAAAAIAAYagAAAAEIM2dcnk6S2R9fy9wpOqToprHz9QZVtQk2qB8xM6b1j5Eq+NQgbBv8LoFj6E/Cq+/Q==", "ab2788e5-53ba-45d7-ac57-bdf69fa90496" });
        }
    }
}
