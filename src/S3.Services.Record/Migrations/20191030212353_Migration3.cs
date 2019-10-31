using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Record.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    StudentName = table.Column<string>(maxLength: 100, nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false),
                    ClassName = table.Column<string>(maxLength: 20, nullable: false),
                    Subject = table.Column<string>(maxLength: 30, nullable: false),
                    ExamType = table.Column<string>(maxLength: 30, nullable: false),
                    Term = table.Column<int>(nullable: false),
                    Session = table.Column<int>(nullable: false),
                    Mark = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentScores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentScores");
        }
    }
}
