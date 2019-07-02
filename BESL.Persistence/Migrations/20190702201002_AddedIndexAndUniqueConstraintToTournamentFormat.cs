using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class AddedIndexAndUniqueConstraintToTournamentFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TournamentFormats_Name_GameId",
                table: "TournamentFormats",
                columns: new[] { "Name", "GameId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TournamentFormats_Name_GameId",
                table: "TournamentFormats");
        }
    }
}
