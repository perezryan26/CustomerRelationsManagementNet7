using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class newAdmin3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87d97800-99b0-4df4-b06e-fe694e99c433");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87d97869-99b0-4df4-b06e-fe694e99c433", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ecc5ae0-a811-4c9b-a7ad-8210de86b1fc", "AQAAAAIAAYagAAAAEKHIST1JNgj6pUrp/gPGy+xAvfxK2wUzMkCVbP08y+R7pvYkP5SSr7Ul++ubl9Y9hQ==", "eea4d8d8-8260-448f-b606-87105634d64e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87d97869-99b0-4df4-b06e-fe694e99c433");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d47ac51-4ae0-4ab2-bc87-b2b5ba0cea4e", "AQAAAAIAAYagAAAAEPKuSat9DspoIY44mxwpDz98OQYtfMzmQnY+nAwY06PFz+IRJx+RfEyN0GPOUQ36tg==", "8aff7f65-b9c6-4595-8c8b-8c5bc7da95d9" });
        }
    }
}
