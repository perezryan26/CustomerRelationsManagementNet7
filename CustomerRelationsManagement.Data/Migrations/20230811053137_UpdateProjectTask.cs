using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDue",
                table: "ProjectTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TaskPriority",
                table: "ProjectTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03068c77-9a5d-4d27-b75e-b8af3d6807df", "AQAAAAIAAYagAAAAEBBOH9WvzfRtnnZSsft1Oh+4NPsKXFUNB6uEzXjMlJup8rk5lieWbmuuHVU5FUC0hA==", "390d4d8a-6311-488f-89f6-16ee247df76d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDue",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "TaskPriority",
                table: "ProjectTasks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d97869-99c0-4df4-b011-fe094e99c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02eca14a-e7a0-47ee-beb6-d704744e9378", "AQAAAAIAAYagAAAAEDn++feisLIfhiUnOLFCcO4W0Zfe48q9u0Zw+k9L5/HaT8XeD8u7WJbaEQrCH9et3A==", "8b52401f-6419-4119-8e1f-d91f2a581fad" });
        }
    }
}
