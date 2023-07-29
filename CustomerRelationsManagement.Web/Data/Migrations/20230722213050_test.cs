using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Clients_ClientId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Deals");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "786b1b1d-a120-4ac5-9f69-9dba32424d14", "AQAAAAIAAYagAAAAEEGMW+/nQN0BELSjiLyAZPWbGuEmavJxGW/+oJdTchBrTF5PIws8XixO/Mr1j38qjQ==", "91cbe958-96cc-400a-bc4c-7c65064f0c23" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Clients_ClientId",
                table: "Deals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Clients_ClientId",
                table: "Deals");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Deals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                values: new object[] { "b3c92c68-5d84-460c-a03c-1788708a1f34", "AQAAAAIAAYagAAAAEENAHjCa6CwBBLeOrM1toSPjhAsRUbVK1j4EaU/vqNVAZzrJ2/F9tefm5cCtXm4Hkg==", "c546f1be-fcd9-4858-9f14-d9d1250f53db" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Clients_ClientId",
                table: "Deals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
