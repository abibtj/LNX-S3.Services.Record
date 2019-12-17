using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Record.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "SecondExamPercentage",
                table: "Rules",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "HomeworkPercentage",
                table: "Rules",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "ClassActivitiesPercentage",
                table: "Rules",
                nullable: true,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "SecondExamPercentage",
                table: "Rules",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "HomeworkPercentage",
                table: "Rules",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ClassActivitiesPercentage",
                table: "Rules",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
