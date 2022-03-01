using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class LibraryMigration_Book_Requests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookRequest",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    IsBookReturned = table.Column<bool>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRequest", x => new { x.BookId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_BookRequest_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRequest_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRequest_StudentId",
                table: "BookRequest",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRequest");
        }
    }
}
