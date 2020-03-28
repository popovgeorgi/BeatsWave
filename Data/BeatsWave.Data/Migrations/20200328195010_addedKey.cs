using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class addedKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Beats",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Beats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
