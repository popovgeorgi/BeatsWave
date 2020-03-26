namespace BeatsWave.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class addBeatUrlPropertyInBeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BeatPublicId",
                table: "Beats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeatThumbnailUrl",
                table: "Beats",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Length",
                table: "Beats",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeatPublicId",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "BeatThumbnailUrl",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Beats");
        }
    }
}
