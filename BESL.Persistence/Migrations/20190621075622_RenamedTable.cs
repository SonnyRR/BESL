using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class RenamedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_CompetitionTableResults_CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "CompetitionTableResults");

            migrationBuilder.CreateTable(
                name: "TeamTableResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    TeamId = table.Column<int>(nullable: false),
                    CompetitionTableId = table.Column<int>(nullable: false),
                    TotalPoints = table.Column<int>(nullable: false),
                    PenaltyPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTableResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamTableResults_CompetitionTables_CompetitionTableId",
                        column: x => x.CompetitionTableId,
                        principalTable: "CompetitionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamTableResults_Teams_Id",
                        column: x => x.Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamTableResults_CompetitionTableId",
                table: "TeamTableResults",
                column: "CompetitionTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamTableResults_CompetitionTableResultId",
                table: "Matches",
                column: "CompetitionTableResultId",
                principalTable: "TeamTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamTableResults_CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "TeamTableResults");

            migrationBuilder.CreateTable(
                name: "CompetitionTableResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CompetitionTableId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    PenaltyPoints = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TotalPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTableResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionTableResults_CompetitionTables_CompetitionTableId",
                        column: x => x.CompetitionTableId,
                        principalTable: "CompetitionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionTableResults_Teams_Id",
                        column: x => x.Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTableResults_CompetitionTableId",
                table: "CompetitionTableResults",
                column: "CompetitionTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionTableResults_CompetitionTableResultId",
                table: "Matches",
                column: "CompetitionTableResultId",
                principalTable: "CompetitionTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
