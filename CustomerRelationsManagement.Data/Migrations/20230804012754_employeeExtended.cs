using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class employeeExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PositionId", "SecurityStamp" },
                values: new object[] { "ab909173-8238-4c13-9f18-dc3e1e366e93", "AQAAAAIAAYagAAAAELySE+9mhZqPc7xZha5R8ABUPTy/fb1dj8/ZEtpSdkVvjq/dZUbJtZoTsC/1DsXkkQ==", 0, "c9024855-04ab-4981-8bd3-ae7de34a2457" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_PositionId",
                table: "AspNetUsers",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Positions_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c645fa21-3019-41b2-8cf4-b3e4d31ea2db", "AQAAAAIAAYagAAAAEDBlvKurOxtLS3P++Bu8H4ae3bNfTx4mKBSfcyVB69GyeVOLd+eAAA/kc9vsq/RTfQ==", "b8707ab1-fce3-4e3f-83ad-21a446023871" });
        }
    }
}
