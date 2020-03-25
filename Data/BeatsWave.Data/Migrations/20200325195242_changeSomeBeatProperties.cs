using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class changeSomeBeatProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProducerId",
                table: "Beats",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CloudinaryImageId",
                table: "Beats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beats_CloudinaryImageId",
                table: "Beats",
                column: "CloudinaryImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beats_CloudinaryImages_CloudinaryImageId",
                table: "Beats",
                column: "CloudinaryImageId",
                principalTable: "CloudinaryImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beats_CloudinaryImages_CloudinaryImageId",
                table: "Beats");

            migrationBuilder.DropIndex(
                name: "IX_Beats_CloudinaryImageId",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "CloudinaryImageId",
                table: "Beats");

            migrationBuilder.AlterColumn<string>(
                name: "ProducerId",
                table: "Beats",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
