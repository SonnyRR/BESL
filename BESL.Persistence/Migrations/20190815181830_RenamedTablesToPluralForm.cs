using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class RenamedTablesToPluralForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_PlayWeek_PlayWeekId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayWeek_TournamentTables_TournamentTableId",
                table: "PlayWeek");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamInvite_AspNetUsers_PlayerId",
                table: "TeamInvite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamInvite",
                table: "TeamInvite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayWeek",
                table: "PlayWeek");

            migrationBuilder.RenameTable(
                name: "TeamInvite",
                newName: "TeamInvites");

            migrationBuilder.RenameTable(
                name: "PlayWeek",
                newName: "PlayWeeks");

            migrationBuilder.RenameIndex(
                name: "IX_TeamInvite_PlayerId",
                table: "TeamInvites",
                newName: "IX_TeamInvites_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayWeek_TournamentTableId",
                table: "PlayWeeks",
                newName: "IX_PlayWeeks_TournamentTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamInvites",
                table: "TeamInvites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayWeeks",
                table: "PlayWeeks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_PlayWeeks_PlayWeekId",
                table: "Matches",
                column: "PlayWeekId",
                principalTable: "PlayWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayWeeks_TournamentTables_TournamentTableId",
                table: "PlayWeeks",
                column: "TournamentTableId",
                principalTable: "TournamentTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamInvites_AspNetUsers_PlayerId",
                table: "TeamInvites",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_PlayWeeks_PlayWeekId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayWeeks_TournamentTables_TournamentTableId",
                table: "PlayWeeks");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamInvites_AspNetUsers_PlayerId",
                table: "TeamInvites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamInvites",
                table: "TeamInvites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayWeeks",
                table: "PlayWeeks");

            migrationBuilder.RenameTable(
                name: "TeamInvites",
                newName: "TeamInvite");

            migrationBuilder.RenameTable(
                name: "PlayWeeks",
                newName: "PlayWeek");

            migrationBuilder.RenameIndex(
                name: "IX_TeamInvites_PlayerId",
                table: "TeamInvite",
                newName: "IX_TeamInvite_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayWeeks_TournamentTableId",
                table: "PlayWeek",
                newName: "IX_PlayWeek_TournamentTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamInvite",
                table: "TeamInvite",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayWeek",
                table: "PlayWeek",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_PlayWeek_PlayWeekId",
                table: "Matches",
                column: "PlayWeekId",
                principalTable: "PlayWeek",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayWeek_TournamentTables_TournamentTableId",
                table: "PlayWeek",
                column: "TournamentTableId",
                principalTable: "TournamentTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamInvite_AspNetUsers_PlayerId",
                table: "TeamInvite",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
