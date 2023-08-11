using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Admin4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b011-fe094e99c111" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "87d97869-99b0-4df4-b06e-fe694e99c433", "87d97869-99c0-4df4-b011-fe094e99c111" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02eca14a-e7a0-47ee-beb6-d704744e9378", "AQAAAAIAAYagAAAAEDn++feisLIfhiUnOLFCcO4W0Zfe48q9u0Zw+k9L5/HaT8XeD8u7WJbaEQrCH9et3A==", "8b52401f-6419-4119-8e1f-d91f2a581fad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "87d97869-99b0-4df4-b06e-fe694e99c433", "87d97869-99c0-4df4-b011-fe094e99c111" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "87d97800-99b0-4df4-b06e-fe694e99c433", "87d97800-99c0-4df4-b011-fe094e99c111" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ecc5ae0-a811-4c9b-a7ad-8210de86b1fc", "AQAAAAIAAYagAAAAEKHIST1JNgj6pUrp/gPGy+xAvfxK2wUzMkCVbP08y+R7pvYkP5SSr7Ul++ubl9Y9hQ==", "eea4d8d8-8260-448f-b606-87105634d64e" });
        }
    }
}
