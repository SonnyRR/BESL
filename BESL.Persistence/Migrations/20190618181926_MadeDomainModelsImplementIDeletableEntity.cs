using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BESL.Persistence.Migrations
{
    public partial class MadeDomainModelsImplementIDeletableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PlayerTeams",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PlayerTeams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PlayerMatch",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PlayerMatch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Matches",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Games",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CompetitionFormat",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CompetitionFormat",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Competition",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Competition",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PlayerTeams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PlayerTeams");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PlayerMatch");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PlayerMatch");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CompetitionFormat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CompetitionFormat");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Competition");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Competition");
        }
    }
}
