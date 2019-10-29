using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Record.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassSubjectScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false),
                    ClassName = table.Column<string>(maxLength: 20, nullable: false),
                    Subject = table.Column<string>(maxLength: 50, nullable: false),
                    ExamType = table.Column<string>(maxLength: 30, nullable: false),
                    Term = table.Column<int>(nullable: false),
                    Session = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjectScores", x => x.Id);
                });

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
                    ClassParticipationPercentage = table.Column<float>(nullable: false),
                    A_DistinctionPoint = table.Column<int>(nullable: false),
                    B_VeryGoodPoint = table.Column<int>(nullable: false),
                    C_CreditPoint = table.Column<int>(nullable: false),
                    P_PassPoint = table.Column<int>(nullable: false),
                    F_FailPoint = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    StudentName = table.Column<string>(maxLength: 100, nullable: false),
                    Mark = table.Column<float>(nullable: false),
                    ClassSubjectScoresId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_ClassSubjectScores_ClassSubjectScoresId",
                        column: x => x.ClassSubjectScoresId,
                        principalTable: "ClassSubjectScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ClassSubjectScoresId",
                table: "Scores",
                column: "ClassSubjectScoresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "ClassSubjectScores");
        }
    }
}
