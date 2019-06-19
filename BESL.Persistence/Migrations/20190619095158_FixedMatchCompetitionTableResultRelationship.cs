using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class FixedMatchCompetitionTableResultRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamTableResultId",
                table: "Matches",
                newName: "CompetitionTableResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompetitionTableResultId",
                table: "Matches",
                newName: "TeamTableResultId");
        }
    }
}
