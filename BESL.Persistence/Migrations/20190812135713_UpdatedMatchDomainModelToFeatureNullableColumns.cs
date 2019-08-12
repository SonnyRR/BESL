using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class UpdatedMatchDomainModelToFeatureNullableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WinnerTeamId",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledDate",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WinnerTeamId",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledDate",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
