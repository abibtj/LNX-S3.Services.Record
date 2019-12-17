using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Record.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    CAPercentage = table.Column<float>(nullable: false),
                    FirstExamPercentage = table.Column<float>(nullable: false),
                    SecondExamPercentage = table.Column<float>(nullable: false),
                    HomeworkPercentage = table.Column<float>(nullable: false),
                    ClassActivitiesPercentage = table.Column<float>(nullable: false),
                    A_DistinctionCutoff = table.Column<float>(nullable: false),
                    B_VeryGoodCutoff = table.Column<float>(nullable: false),
                    C_CreditCutoff = table.Column<float>(nullable: false),
                    P_PassCutoff = table.Column<float>(nullable: false),
                    F_FailCutoff = table.Column<float>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

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
                    Term = table.Column<string>(maxLength: 15, nullable: false),
                    Session = table.Column<int>(nullable: false),
                    Mark = table.Column<float>(nullable: false),
                    RuleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentScores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "StudentScores");
        }
    }
}
