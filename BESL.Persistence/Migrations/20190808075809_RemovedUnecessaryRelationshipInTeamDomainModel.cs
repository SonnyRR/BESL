using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class RemovedUnecessaryRelationshipInTeamDomainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamTableResults_CurrentActiveTeamTableResultId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentActiveTeamTableResultId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CurrentActiveTeamTableResultId",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentActiveTeamTableResultId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentActiveTeamTableResultId",
                table: "Teams",
                column: "CurrentActiveTeamTableResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamTableResults_CurrentActiveTeamTableResultId",
                table: "Teams",
                column: "CurrentActiveTeamTableResultId",
                principalTable: "TeamTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
