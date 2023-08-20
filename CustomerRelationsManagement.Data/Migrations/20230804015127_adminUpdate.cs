using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class adminUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2dd9a624-6960-4e99-b41d-a4c0ae2c4732", "admin@admin.com", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEA8+D+uWQrY9tt1pvB8Yi+EGVw2C9dh1eZwe8CxnQW2tIurfS85iy98h9iVEfiksDg==", "b011d28b-72a0-4702-8e13-770d0169e051", "admin@admin.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "ab909173-8238-4c13-9f18-dc3e1e366e93", "admin@email.com", "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAELySE+9mhZqPc7xZha5R8ABUPTy/fb1dj8/ZEtpSdkVvjq/dZUbJtZoTsC/1DsXkkQ==", "c9024855-04ab-4981-8bd3-ae7de34a2457", "admin@email.com" });
        }
    }
}
