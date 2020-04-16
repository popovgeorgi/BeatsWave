using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class removeUselessEnitities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloudinaryImages_AspNetUsers_ProfilePictureUploaderId",
                table: "CloudinaryImages");

            migrationBuilder.DropIndex(
                name: "IX_CloudinaryImages_ProfilePictureUploaderId",
                table: "CloudinaryImages");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUploaderId",
                table: "CloudinaryImages");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUploaderId",
                table: "CloudinaryImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CloudinaryImages_ProfilePictureUploaderId",
                table: "CloudinaryImages",
                column: "ProfilePictureUploaderId",
                unique: true,
                filter: "[ProfilePictureUploaderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CloudinaryImages_AspNetUsers_ProfilePictureUploaderId",
                table: "CloudinaryImages",
                column: "ProfilePictureUploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
