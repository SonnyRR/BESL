using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class RefactoringsInModelsAndFixedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TableResults_Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatch_Matches_MatchId",
                table: "PlayerMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatch_AspNetUsers_PlayerId",
                table: "PlayerMatch");

            migrationBuilder.DropTable(
                name: "TableResults");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerMatch",
                table: "PlayerMatch");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "PlayerMatch",
                newName: "PlayerMatches");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerMatch_MatchId",
                table: "PlayerMatches",
                newName: "IX_PlayerMatches_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerMatches",
                table: "PlayerMatches",
                columns: new[] { "PlayerId", "MatchId" });

            migrationBuilder.CreateTable(
                name: "CompetitionTableResults",
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
                name: "IX_Matches_WinnerTeamId",
                table: "Matches",
                column: "WinnerTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTableResults_CompetitionTableId",
                table: "CompetitionTableResults",
                column: "CompetitionTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_CompetitionTableResults_Id",
                table: "Matches",
                column: "Id",
                principalTable: "CompetitionTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_WinnerTeamId",
                table: "Matches",
                column: "WinnerTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatches_AspNetUsers_PlayerId",
                table: "PlayerMatches",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_CompetitionTableResults_Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_WinnerTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_AspNetUsers_PlayerId",
                table: "PlayerMatches");

            migrationBuilder.DropTable(
                name: "CompetitionTableResults");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinnerTeamId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerMatches",
                table: "PlayerMatches");

            migrationBuilder.RenameTable(
                name: "PlayerMatches",
                newName: "PlayerMatch");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerMatches_MatchId",
                table: "PlayerMatch",
                newName: "IX_PlayerMatch_MatchId");

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerMatch",
                table: "PlayerMatch",
                columns: new[] { "PlayerId", "MatchId" });

            migrationBuilder.CreateTable(
                name: "TableResults",
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
                    table.PrimaryKey("PK_TableResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableResults_CompetitionTables_CompetitionTableId",
                        column: x => x.CompetitionTableId,
                        principalTable: "CompetitionTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TableResults_Teams_Id",
                        column: x => x.Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TableResults_CompetitionTableId",
                table: "TableResults",
                column: "CompetitionTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TableResults_Id",
                table: "Matches",
                column: "Id",
                principalTable: "TableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatch_Matches_MatchId",
                table: "PlayerMatch",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatch_AspNetUsers_PlayerId",
                table: "PlayerMatch",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
