using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingPeriodToAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c104c708-1549-48dd-80a2-b1f38b41741f", "AQAAAAIAAYagAAAAEIV7g0KQglgAEk1p+0eDJcw6PLnnKgNs/y8pP3IPYMNe+tTumAR2n6i8QRhKk7eXNQ==", "84c4c254-e511-427b-8321-00dac2dbfe1d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97800-99c0-4df4-b06e-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "043170e0-e27a-42ae-85c2-e89c5bd6fade", "AQAAAAIAAYagAAAAEHwBdtpgHZNS28kN25TcHBGYq5K29ZMHROn7Z+atgeBLHfdivWSb1QWm9w4S/4af9g==", "2f34b503-0c6f-44b0-9111-2a455323fa72" });
        }
    }
}
