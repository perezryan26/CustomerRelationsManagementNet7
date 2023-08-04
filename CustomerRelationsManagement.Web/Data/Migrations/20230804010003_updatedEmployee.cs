using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Names",
                table: "Positions",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "Department", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c645fa21-3019-41b2-8cf4-b3e4d31ea2db", null, "AQAAAAIAAYagAAAAEDBlvKurOxtLS3P++Bu8H4ae3bNfTx4mKBSfcyVB69GyeVOLd+eAAA/kc9vsq/RTfQ==", "b8707ab1-fce3-4e3f-83ad-21a446023871" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Positions",
                newName: "Names");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4c85785-1f29-46f2-b0a9-a17b53b819ed", "AQAAAAIAAYagAAAAEAN1Fj+KRFkV/cb656pMNiFEJ4h6HJ3qVMiPzmKUCJE9m5g8w0DUyhA6ArrsL9h4Cw==", "76b9ee4c-5783-475a-a26f-267f4cb15a4a" });
        }
    }
}
