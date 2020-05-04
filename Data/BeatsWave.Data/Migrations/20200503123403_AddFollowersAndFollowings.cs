using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatsWave.Data.Migrations
{
    public partial class AddFollowersAndFollowings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Followings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followers_IsDeleted",
                table: "Followers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_IsDeleted",
                table: "Followings",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "Followings");

            migrationBuilder.AddColumn<int>(
                name: "FollowInfoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowInfoId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FollowInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
