using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Deals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f99b4ae7-09b9-4ac4-b810-537b7145866a", "AQAAAAIAAYagAAAAEG/lXrNvnJyafUW76DRI+TK4i2vCac0gIM9P4bs6OCpHomMW9Pujlsc531wDiVkDNQ==", "62e20635-e083-4a82-9ede-049c04c9076e" });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_ClientId",
                table: "Deals",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Clients_ClientId",
                table: "Deals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Clients_ClientId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_ClientId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Deals");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b40a98a-1618-4ed2-a38b-23c43f1b31e3", "AQAAAAIAAYagAAAAEElzvyqLqIKHL+qBlPbHOeI/hfqV8xNvckiDjMbxpKpxBoG5vsVkjFuzDFcrAtHQSQ==", "461adeac-90aa-4bda-9f72-2c6507e4520e" });
        }
    }
}
