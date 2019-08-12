using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class UpdatedTeamTableResultDomainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamTableResults_TeamTableResultId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamTableResultId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamTableResultId",
                table: "Matches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamTableResultId",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamTableResultId",
                table: "Matches",
                column: "TeamTableResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamTableResults_TeamTableResultId",
                table: "Matches",
                column: "TeamTableResultId",
                principalTable: "TeamTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
