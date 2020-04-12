using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class addProfilePictureToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUploaderId",
                table: "CloudinaryImages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
