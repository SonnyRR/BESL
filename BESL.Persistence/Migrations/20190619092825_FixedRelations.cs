using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class FixedRelations : Migration
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
                name: "FK_CompetitionTable_Competition_CompetitionId",
                table: "CompetitionTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamTableResult_Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_CompetitionTable_CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TeamTableResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionTable",
                table: "CompetitionTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionFormat",
                table: "CompetitionFormat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competition",
                table: "Competition");

            migrationBuilder.RenameTable(
                name: "CompetitionTable",
                newName: "CompetitionTables");

            migrationBuilder.RenameTable(
                name: "CompetitionFormat",
                newName: "CompetitionFormats");

            migrationBuilder.RenameTable(
                name: "Competition",
                newName: "Competitions");

            migrationBuilder.RenameIndex(
                name: "IX_CompetitionTable_CompetitionId",
                table: "CompetitionTables",
                newName: "IX_CompetitionTables_CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Competition_GameId",
                table: "Competitions",
                newName: "IX_Competitions_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Competition_FormatId",
                table: "Competitions",
                newName: "IX_Competitions_FormatId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompetitionTables",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionTables",
                table: "CompetitionTables",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionFormats",
                table: "CompetitionFormats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TableResults",
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
                name: "IX_TableResults_CompetitionTableId",
                table: "TableResults",
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
                name: "FK_CompetitionTables_Competitions_CompetitionId",
                table: "CompetitionTables",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TableResults_Id",
                table: "Matches",
                column: "Id",
                principalTable: "TableResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_CompetitionTables_CurrentActiveCompetitionTableId",
                table: "Teams",
                column: "CurrentActiveCompetitionTableId",
                principalTable: "CompetitionTables",
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
                name: "FK_CompetitionTables_Competitions_CompetitionId",
                table: "CompetitionTables");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TableResults_Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_CompetitionTables_CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TableResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionTables",
                table: "CompetitionTables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competitions",
                table: "Competitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionFormats",
                table: "CompetitionFormats");

            migrationBuilder.RenameTable(
                name: "CompetitionTables",
                newName: "CompetitionTable");

            migrationBuilder.RenameTable(
                name: "Competitions",
                newName: "Competition");

            migrationBuilder.RenameTable(
                name: "CompetitionFormats",
                newName: "CompetitionFormat");

            migrationBuilder.RenameIndex(
                name: "IX_CompetitionTables_CompetitionId",
                table: "CompetitionTable",
                newName: "IX_CompetitionTable_CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Competitions_GameId",
                table: "Competition",
                newName: "IX_Competition_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Competitions_FormatId",
                table: "Competition",
                newName: "IX_Competition_FormatId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompetitionTable",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionTable",
                table: "CompetitionTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competition",
                table: "Competition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionFormat",
                table: "CompetitionFormat",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TeamTableResult",
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
                    table.PrimaryKey("PK_TeamTableResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamTableResult_CompetitionTable_CompetitionTableId",
                        column: x => x.CompetitionTableId,
                        principalTable: "CompetitionTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamTableResult_Teams_Id",
                        column: x => x.Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamTableResult_CompetitionTableId",
                table: "TeamTableResult",
                column: "CompetitionTableId");

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
                name: "FK_CompetitionTable_Competition_CompetitionId",
                table: "CompetitionTable",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamTableResult_Id",
                table: "Matches",
                column: "Id",
                principalTable: "TeamTableResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_CompetitionTable_CurrentActiveCompetitionTableId",
                table: "Teams",
                column: "CurrentActiveCompetitionTableId",
                principalTable: "CompetitionTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
