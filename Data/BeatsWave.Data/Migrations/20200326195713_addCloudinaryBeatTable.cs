using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class addCloudinaryBeatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeatPublicId",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "BeatThumbnailUrl",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "BeatUrl",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Beats");

            migrationBuilder.AddColumn<int>(
                name: "CloudinaryBeatId",
                table: "Beats",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CloudinaryBeatUrlId",
                table: "Beats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CloudinaryBeats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    BeatPublicId = table.Column<string>(nullable: true),
                    BeatUrl = table.Column<string>(nullable: true),
                    BeatThumbnailUrl = table.Column<string>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    UploaderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudinaryBeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CloudinaryBeats_AspNetUsers_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beats_CloudinaryBeatId",
                table: "Beats",
                column: "CloudinaryBeatId");

            migrationBuilder.CreateIndex(
                name: "IX_CloudinaryBeats_IsDeleted",
                table: "CloudinaryBeats",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CloudinaryBeats_UploaderId",
                table: "CloudinaryBeats",
                column: "UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beats_CloudinaryBeats_CloudinaryBeatId",
                table: "Beats",
                column: "CloudinaryBeatId",
                principalTable: "CloudinaryBeats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beats_CloudinaryBeats_CloudinaryBeatId",
                table: "Beats");

            migrationBuilder.DropTable(
                name: "CloudinaryBeats");

            migrationBuilder.DropIndex(
                name: "IX_Beats_CloudinaryBeatId",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "CloudinaryBeatId",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "CloudinaryBeatUrlId",
                table: "Beats");

            migrationBuilder.AddColumn<string>(
                name: "BeatPublicId",
                table: "Beats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeatThumbnailUrl",
                table: "Beats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeatUrl",
                table: "Beats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Length",
                table: "Beats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
