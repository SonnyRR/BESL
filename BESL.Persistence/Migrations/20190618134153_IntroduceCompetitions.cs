using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class IntroduceCompetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionType");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompetitionFormat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TotalPlayersCount = table.Column<int>(nullable: false),
                    TeamPlayersCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionFormat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 35, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    FormatId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competition_CompetitionFormat_FormatId",
                        column: x => x.FormatId,
                        principalTable: "CompetitionFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competition_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompetitionId",
                table: "Teams",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competition_FormatId",
                table: "Competition",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Competition_GameId",
                table: "Competition",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Competition_CompetitionId",
                table: "Teams",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Competition_CompetitionId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "CompetitionFormat");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CompetitionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Teams");

            migrationBuilder.CreateTable(
                name: "CompetitionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TeamPlayersCount = table.Column<int>(nullable: false),
                    TotalPlayersCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionType_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionType_GameId",
                table: "CompetitionType",
                column: "GameId");
        }
    }
}
