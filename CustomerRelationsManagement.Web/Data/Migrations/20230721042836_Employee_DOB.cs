using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationsManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Employee_DOB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "AspNetUsers",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "AspNetUsers",
                newName: "CreatedDate");
        }
    }
}
