using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class FixedMatchRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_CompetitionTableResults_Id",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionTableResultId",
                table: "Matches",
                column: "CompetitionTableResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionTableResults_CompetitionTableResultId",
                table: "Matches",
                column: "CompetitionTableResultId",
                principalTable: "CompetitionTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_CompetitionTableResults_CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionTableResults_Id",
                table: "Matches",
                column: "Id",
                principalTable: "CompetitionTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
