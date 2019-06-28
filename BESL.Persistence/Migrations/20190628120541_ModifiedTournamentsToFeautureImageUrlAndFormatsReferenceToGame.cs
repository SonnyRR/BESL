using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class ModifiedTournamentsToFeautureImageUrlAndFormatsReferenceToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TournamentImageUrl",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "TournamentFormats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentFormats_GameId",
                table: "TournamentFormats",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentFormats_Games_GameId",
                table: "TournamentFormats",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentFormats_Games_GameId",
                table: "TournamentFormats");

            migrationBuilder.DropIndex(
                name: "IX_TournamentFormats_GameId",
                table: "TournamentFormats");

            migrationBuilder.DropColumn(
                name: "TournamentImageUrl",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "TournamentFormats");
        }
    }
}
