using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class IntroducedPlayWeek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayWeekId",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlayWeek",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    TournamentTableId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayWeek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayWeek_TournamentTables_TournamentTableId",
                        column: x => x.TournamentTableId,
                        principalTable: "TournamentTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayWeekId",
                table: "Matches",
                column: "PlayWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayWeek_TournamentTableId",
                table: "PlayWeek",
                column: "TournamentTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_PlayWeek_PlayWeekId",
                table: "Matches",
                column: "PlayWeekId",
                principalTable: "PlayWeek",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_PlayWeek_PlayWeekId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "PlayWeek");

            migrationBuilder.DropIndex(
                name: "IX_Matches_PlayWeekId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "PlayWeekId",
                table: "Matches");
        }
    }
}
