using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class LibraryMigration_Book_Authors_properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Career",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Book",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Author",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Career",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Author");
        }
    }
}
