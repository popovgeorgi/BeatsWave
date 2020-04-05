using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class makeCloudinaryImageBaseDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CloudinaryImages",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CloudinaryImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CloudinaryImages_IsDeleted",
                table: "CloudinaryImages",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CloudinaryImages_IsDeleted",
                table: "CloudinaryImages");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CloudinaryImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CloudinaryImages");
        }
    }
}
