using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class addFollowInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowInfoId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowInfoId1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FollowInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FollowInfoId",
                table: "AspNetUsers",
                column: "FollowInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FollowInfoId1",
                table: "AspNetUsers",
                column: "FollowInfoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FollowInfos_FollowInfoId",
                table: "AspNetUsers",
                column: "FollowInfoId",
                principalTable: "FollowInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FollowInfos_FollowInfoId1",
                table: "AspNetUsers",
                column: "FollowInfoId1",
                principalTable: "FollowInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FollowInfos_FollowInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FollowInfos_FollowInfoId1",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FollowInfos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FollowInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FollowInfoId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FollowInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FollowInfoId1",
                table: "AspNetUsers");
        }
    }
}
