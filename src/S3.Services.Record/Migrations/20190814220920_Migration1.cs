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
                    ClassParticipationPercentage = table.Column<float>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}
