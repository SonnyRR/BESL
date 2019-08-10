using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class RemovedTeamInviteUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamInvite_TeamId_PlayerId",
                table: "TeamInvite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TeamInvite_TeamId_PlayerId",
                table: "TeamInvite",
                columns: new[] { "TeamId", "PlayerId" },
                unique: true,
                filter: "[PlayerId] IS NOT NULL");
        }
    }
}
