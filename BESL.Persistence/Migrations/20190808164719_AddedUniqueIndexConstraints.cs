using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class AddedUniqueIndexConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamTableResults_TeamId",
                table: "TeamTableResults");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_Name",
                table: "Tournaments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamTableResults_TeamId_TournamentTableId",
                table: "TeamTableResults",
                columns: new[] { "TeamId", "TournamentTableId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                table: "Teams",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                table: "Games",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tournaments_Name",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_TeamTableResults_TeamId_TournamentTableId",
                table: "TeamTableResults");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Name",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Games_Name",
                table: "Games");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTableResults_TeamId",
                table: "TeamTableResults",
                column: "TeamId");
        }
    }
}
