using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class RemovedMatchRelationshipWithTeamTableResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TeamTableResultId",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TeamTableResultId",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
