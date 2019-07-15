using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class ChangedTeamDomainModelToFeatureTournamentFormatInsteadOfGameId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TournamentFormatId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TournamentFormatId",
                table: "Teams",
                column: "TournamentFormatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TournamentFormats_TournamentFormatId",
                table: "Teams",
                column: "TournamentFormatId",
                principalTable: "TournamentFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TournamentFormats_TournamentFormatId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TournamentFormatId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TournamentFormatId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
