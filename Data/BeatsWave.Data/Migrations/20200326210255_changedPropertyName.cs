using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class changedPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryBeatUrlId",
                table: "Beats");

            migrationBuilder.AlterColumn<int>(
                name: "CloudinaryBeatId",
                table: "Beats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CloudinaryBeatId",
                table: "Beats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CloudinaryBeatUrlId",
                table: "Beats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
