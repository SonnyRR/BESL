using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class UpdateTeamDomainModelToIncludeCurrentCompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Competition_CompetitionId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CompetitionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CurrentCompetitionId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentCompetitionId",
                table: "Teams",
                column: "CurrentCompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Competition_CurrentCompetitionId",
                table: "Teams",
                column: "CurrentCompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Competition_CurrentCompetitionId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentCompetitionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CurrentCompetitionId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompetitionId",
                table: "Teams",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Competition_CompetitionId",
                table: "Teams",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
