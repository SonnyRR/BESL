using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class IntroduceTeamTableResultAndCompetitionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competition_CompetitionFormat_FormatId",
                table: "Competition");

            migrationBuilder.DropForeignKey(
                name: "FK_Competition_Games_GameId",
                table: "Competition");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatch_Matches_MatchId",
                table: "PlayerMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatch_AspNetUsers_PlayerId",
                table: "PlayerMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Competition_CurrentCompetitionId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerMatch",
                table: "PlayerMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionFormat",
                table: "CompetitionFormat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competition",
                table: "Competition");

            migrationBuilder.RenameTable(
                name: "PlayerMatch",
                newName: "PlayerMatches");

            migrationBuilder.RenameTable(
                name: "CompetitionFormat",
                newName: "CompetitionFormats");

            migrationBuilder.RenameTable(
                name: "Competition",
                newName: "Competitions");

            migrationBuilder.RenameColumn(
                name: "CurrentCompetitionId",
                table: "Teams",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CurrentCompetitionId",
                table: "Teams",
                newName: "IX_Teams_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerMatch_MatchId",
                table: "PlayerMatches",
                newName: "IX_PlayerMatches_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Competition_GameId",
                table: "Competitions",
                newName: "IX_Competitions_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Competition_FormatId",
                table: "Competitions",
                newName: "IX_Competitions_FormatId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentActiveCompetitionTableId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompetitionTableResultId",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Competitions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerMatches",
                table: "PlayerMatches",
                columns: new[] { "PlayerId", "MatchId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionFormats",
                table: "CompetitionFormats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CompetitionTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    MaxNumberOfTeams = table.Column<int>(nullable: false),
                    CompetitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionTables_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamTableResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                name: "IX_Teams_CurrentActiveCompetitionTableId",
                table: "Teams",
                column: "CurrentActiveCompetitionTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionTableResultId",
                table: "Matches",
                column: "CompetitionTableResultId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTables_CompetitionId",
                table: "CompetitionTables",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTableResults_CompetitionTableId",
                table: "TeamTableResults",
                column: "CompetitionTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_CompetitionFormats_FormatId",
                table: "Competitions",
                column: "FormatId",
                principalTable: "CompetitionFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Games_GameId",
                table: "Competitions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamTableResults_CompetitionTableResultId",
                table: "Matches",
                column: "CompetitionTableResultId",
                principalTable: "TeamTableResults",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_CompetitionTables_CurrentActiveCompetitionTableId",
                table: "Teams",
                column: "CurrentActiveCompetitionTableId",
                principalTable: "CompetitionTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_CompetitionFormats_FormatId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Games_GameId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamTableResults_CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_AspNetUsers_PlayerId",
                table: "PlayerMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_CompetitionTables_CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TeamTableResults");

            migrationBuilder.DropTable(
                name: "CompetitionTables");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerMatches",
                table: "PlayerMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionFormats",
                table: "CompetitionFormats");

            migrationBuilder.DropColumn(
                name: "CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Competitions");

            migrationBuilder.RenameTable(
                name: "PlayerMatches",
                newName: "PlayerMatch");

            migrationBuilder.RenameTable(
                name: "Competitions",
                newName: "Competition");

            migrationBuilder.RenameTable(
                name: "CompetitionFormats",
                newName: "CompetitionFormat");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Teams",
                newName: "CurrentCompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                newName: "IX_Teams_CurrentCompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerMatches_MatchId",
                table: "PlayerMatch",
                newName: "IX_PlayerMatch_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Competitions_GameId",
                table: "Competition",
                newName: "IX_Competition_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Competitions_FormatId",
                table: "Competition",
                newName: "IX_Competition_FormatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerMatch",
                table: "PlayerMatch",
                columns: new[] { "PlayerId", "MatchId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competition",
                table: "Competition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionFormat",
                table: "CompetitionFormat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competition_CompetitionFormat_FormatId",
                table: "Competition",
                column: "FormatId",
                principalTable: "CompetitionFormat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Competition_Games_GameId",
                table: "Competition",
                column: "GameId",
                principalTable: "Games",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Competition_CurrentCompetitionId",
                table: "Teams",
                column: "CurrentCompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
