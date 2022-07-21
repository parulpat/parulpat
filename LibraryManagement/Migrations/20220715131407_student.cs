using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagement.Migrations
{
    public partial class student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "RegisterUser");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "RegisterUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "RegisterUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RegisterUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "RegisterUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "RegisterUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BookViewModel",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false),
                    Copies = table.Column<string>(nullable: true),
                    BookPublication = table.Column<string>(nullable: true),
                    PublisherName = table.Column<string>(nullable: true),
                    ISBNNo = table.Column<string>(nullable: true),
                    CopyRightYear = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    ActiveStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookViewModel", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    studentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentName = table.Column<string>(nullable: true),
                    RollNo = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ProfileImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.studentId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookViewModel");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "RegisterUser");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RegisterUser");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RegisterUser");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "RegisterUser");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RegisterUser");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "RegisterUser",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
