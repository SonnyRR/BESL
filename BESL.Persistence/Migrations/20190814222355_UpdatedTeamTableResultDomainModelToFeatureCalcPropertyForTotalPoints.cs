using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class UpdatedTeamTableResultDomainModelToFeatureCalcPropertyForTotalPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPoints",
                table: "TeamTableResults",
                newName: "Points");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Points",
                table: "TeamTableResults",
                newName: "TotalPoints");
        }
    }
}
