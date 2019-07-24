using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class ImproveDatbaseNormalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TournamentTables_CurrentActiveTournamentTableId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTableResults_Teams_Id",
                table: "TeamTableResults");

            migrationBuilder.RenameColumn(
                name: "CurrentActiveTournamentTableId",
                table: "Teams",
                newName: "CurrentActiveTeamTableResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CurrentActiveTournamentTableId",
                table: "Teams",
                newName: "IX_Teams_CurrentActiveTeamTableResultId");

            // What the fuck entity framework...
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "TeamTableResults",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TeamTableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDropped",
                table: "TeamTableResults",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TeamTableResults_TeamId",
                table: "TeamTableResults",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamTableResults_CurrentActiveTeamTableResultId",
                table: "Teams",
                column: "CurrentActiveTeamTableResultId",
                principalTable: "TeamTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTableResults_Teams_TeamId",
                table: "TeamTableResults",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamTableResults_CurrentActiveTeamTableResultId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTableResults_Teams_TeamId",
                table: "TeamTableResults");

            migrationBuilder.DropIndex(
                name: "IX_TeamTableResults_TeamId",
                table: "TeamTableResults");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TeamTableResults");

            migrationBuilder.DropColumn(
                name: "IsDropped",
                table: "TeamTableResults");

            migrationBuilder.RenameColumn(
                name: "CurrentActiveTeamTableResultId",
                table: "Teams",
                newName: "CurrentActiveTournamentTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CurrentActiveTeamTableResultId",
                table: "Teams",
                newName: "IX_Teams_CurrentActiveTournamentTableId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TeamTableResults",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TournamentTables_CurrentActiveTournamentTableId",
                table: "Teams",
                column: "CurrentActiveTournamentTableId",
                principalTable: "TournamentTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTableResults_Teams_Id",
                table: "TeamTableResults",
                column: "Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
