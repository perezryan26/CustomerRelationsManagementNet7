using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class positionSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4c85785-1f29-46f2-b0a9-a17b53b819ed", "AQAAAAIAAYagAAAAEAN1Fj+KRFkV/cb656pMNiFEJ4h6HJ3qVMiPzmKUCJE9m5g8w0DUyhA6ArrsL9h4Cw==", "76b9ee4c-5783-475a-a26f-267f4cb15a4a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "535f46cb-028c-4aed-b5dc-5e27b9bd3a6e", "AQAAAAIAAYagAAAAEIsA0224tzqRMbyF/1UAvPHFqR/cxVS5xjtRWhKUK5Tkv7VdzqPMmXiOHUPEXgMrSg==", "46a39a2e-d4f3-4903-a88f-c0296a3d8c7f" });
        }
    }
}
