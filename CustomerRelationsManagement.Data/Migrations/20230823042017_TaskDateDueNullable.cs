using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaskDateDueNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDue",
                table: "ProjectTasks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "800a5198-3d7c-4867-9b2b-cec1ef7d4ed0", "AQAAAAIAAYagAAAAEBKW7dk+RCch2UY54ju3JtWO4V7HYZYM1QWm2ueF6dZjMW7zEzu48CPTerJAy92KOA==", "73542986-d591-4068-98b8-909f6ed4f129" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDue",
                table: "ProjectTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c53fedc4-692f-4688-8cce-1be9e0a8aac0", "AQAAAAIAAYagAAAAEGZK7wdlsb1pi/OtG7AQVPZiFkg7wTKzzVHoa+RlO8yHOGXfpjUZ1FQ3/A2HCuqEwg==", "f8a7c1ae-da52-4210-8df5-02d3d864f3c3" });
        }
    }
}
