using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class IntroduceTeamTableResultAndCompetitionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_WinnerTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Competition_CurrentCompetitionId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinnerTeamId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "CurrentCompetitionId",
                table: "Teams",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CurrentCompetitionId",
                table: "Teams",
                newName: "IX_Teams_GameId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentActiveCompetitionTableId",
                table: "Teams",
                nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Matches",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TeamTableResultId",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Competition",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CompetitionTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MaxNumberOfTeams = table.Column<int>(nullable: false),
                    CompetitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionTable_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamTableResult",
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
                name: "IX_Teams_CurrentActiveCompetitionTableId",
                table: "Teams",
                column: "CurrentActiveCompetitionTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionTable_CompetitionId",
                table: "CompetitionTable",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTableResult_CompetitionTableId",
                table: "TeamTableResult",
                column: "CompetitionTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamTableResult_Id",
                table: "Matches",
                column: "Id",
                principalTable: "TeamTableResult",
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
                name: "FK_Teams_CompetitionTable_CurrentActiveCompetitionTableId",
                table: "Teams",
                column: "CurrentActiveCompetitionTableId",
                principalTable: "CompetitionTable",
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
                name: "FK_Matches_TeamTableResult_Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_CompetitionTable_CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TeamTableResult");

            migrationBuilder.DropTable(
                name: "CompetitionTable");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CurrentActiveCompetitionTableId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamTableResultId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Competition");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Teams",
                newName: "CurrentCompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                newName: "IX_Teams_CurrentCompetitionId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerTeamId",
                table: "Matches",
                column: "WinnerTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_WinnerTeamId",
                table: "Matches",
                column: "WinnerTeamId",
                principalTable: "Teams",
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
