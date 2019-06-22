using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class RenamedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamTableResults_CompetitionTableResultId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_CompetitionTables_CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTableResults_CompetitionTables_CompetitionTableId",
                table: "TeamTableResults");

            migrationBuilder.DropTable(
                name: "CompetitionTables");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "CompetitionFormats");

            migrationBuilder.RenameColumn(
                name: "CompetitionTableId",
                table: "TeamTableResults",
                newName: "TournamentTableId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTableResults_CompetitionTableId",
                table: "TeamTableResults",
                newName: "IX_TeamTableResults_TournamentTableId");

            migrationBuilder.RenameColumn(
                name: "CurrentActiveCompetitionTableId",
                table: "Teams",
                newName: "CurrentActiveTournamentTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CurrentActiveCompetitionTableId",
                table: "Teams",
                newName: "IX_Teams_CurrentActiveTournamentTableId");

            migrationBuilder.RenameColumn(
                name: "CompetitionTableResultId",
                table: "Matches",
                newName: "TeamTableResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_CompetitionTableResultId",
                table: "Matches",
                newName: "IX_Matches_TeamTableResultId");

            migrationBuilder.CreateTable(
                name: "TournamentFormats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TotalPlayersCount = table.Column<int>(nullable: false),
                    TeamPlayersCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentFormats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 35, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FormatId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentFormats_FormatId",
                        column: x => x.FormatId,
                        principalTable: "TournamentFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tournaments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTables",
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
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentTables_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_FormatId",
                table: "Tournaments",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_GameId",
                table: "Tournaments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTables_TournamentId",
                table: "TournamentTables",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamTableResults_TeamTableResultId",
                table: "Matches",
                column: "TeamTableResultId",
                principalTable: "TeamTableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TournamentTables_CurrentActiveTournamentTableId",
                table: "Teams",
                column: "CurrentActiveTournamentTableId",
                principalTable: "TournamentTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTableResults_TournamentTables_TournamentTableId",
                table: "TeamTableResults",
                column: "TournamentTableId",
                principalTable: "TournamentTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamTableResults_TeamTableResultId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TournamentTables_CurrentActiveTournamentTableId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTableResults_TournamentTables_TournamentTableId",
                table: "TeamTableResults");

            migrationBuilder.DropTable(
                name: "TournamentTables");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "TournamentFormats");

            migrationBuilder.RenameColumn(
                name: "TournamentTableId",
                table: "TeamTableResults",
                newName: "CompetitionTableId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTableResults_TournamentTableId",
                table: "TeamTableResults",
                newName: "IX_TeamTableResults_CompetitionTableId");

            migrationBuilder.RenameColumn(
                name: "CurrentActiveTournamentTableId",
                table: "Teams",
                newName: "CurrentActiveCompetitionTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CurrentActiveTournamentTableId",
                table: "Teams",
                newName: "IX_Teams_CurrentActiveCompetitionTableId");

            migrationBuilder.RenameColumn(
                name: "TeamTableResultId",
                table: "Matches",
                newName: "CompetitionTableResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_TeamTableResultId",
                table: "Matches",
                newName: "IX_Matches_CompetitionTableResultId");

            migrationBuilder.CreateTable(
                name: "CompetitionFormats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TeamPlayersCount = table.Column<int>(nullable: false),
                    TotalPlayersCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionFormats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    FormatId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitions_CompetitionFormats_FormatId",
                        column: x => x.FormatId,
                        principalTable: "CompetitionFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetitionId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaxNumberOfTeams = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_FormatId",
                table: "Competitions",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_GameId",
                table: "Competitions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTables_CompetitionId",
                table: "CompetitionTables",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamTableResults_CompetitionTableResultId",
                table: "Matches",
                column: "CompetitionTableResultId",
                principalTable: "TeamTableResults",
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
                name: "FK_TeamTableResults_CompetitionTables_CompetitionTableId",
                table: "TeamTableResults",
                column: "CompetitionTableId",
                principalTable: "CompetitionTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
