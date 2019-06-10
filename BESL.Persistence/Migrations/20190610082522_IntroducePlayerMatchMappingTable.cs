using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class IntroducePlayerMatchMappingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Matches_MatchId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeam_AspNetUsers_PlayerId",
                table: "PlayerTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeam_Teams_TeamId",
                table: "PlayerTeam");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MatchId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerTeam",
                table: "PlayerTeam");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "PlayerTeam",
                newName: "PlayerTeams");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerTeam_TeamId",
                table: "PlayerTeams",
                newName: "IX_PlayerTeams_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerTeams",
                table: "PlayerTeams",
                columns: new[] { "PlayerId", "TeamId" });

            migrationBuilder.CreateTable(
                name: "PlayerMatch",
                columns: table => new
                {
                    PlayerId = table.Column<string>(nullable: false),
                    MatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMatch", x => new { x.PlayerId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_PlayerMatch_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerMatch_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatch_MatchId",
                table: "PlayerMatch",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeams_AspNetUsers_PlayerId",
                table: "PlayerTeams",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeams_Teams_TeamId",
                table: "PlayerTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeams_AspNetUsers_PlayerId",
                table: "PlayerTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTeams_Teams_TeamId",
                table: "PlayerTeams");

            migrationBuilder.DropTable(
                name: "PlayerMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerTeams",
                table: "PlayerTeams");

            migrationBuilder.RenameTable(
                name: "PlayerTeams",
                newName: "PlayerTeam");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerTeams_TeamId",
                table: "PlayerTeam",
                newName: "IX_PlayerTeam_TeamId");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerTeam",
                table: "PlayerTeam",
                columns: new[] { "PlayerId", "TeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MatchId",
                table: "AspNetUsers",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Matches_MatchId",
                table: "AspNetUsers",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeam_AspNetUsers_PlayerId",
                table: "PlayerTeam",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTeam_Teams_TeamId",
                table: "PlayerTeam",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
