using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class tripLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SwipeDateTime",
                table: "TripLogs",
                newName: "GetOnDateTime");

            migrationBuilder.RenameColumn(
                name: "OnOrOff",
                table: "TripLogs",
                newName: "UID");

            migrationBuilder.RenameColumn(
                name: "Missing",
                table: "TripLogs",
                newName: "Manual2");

            migrationBuilder.AddColumn<string>(
                name: "Comments2",
                table: "TripLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GetOffDateTime2",
                table: "TripLogs",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude2",
                table: "TripLogs",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location2",
                table: "TripLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude2",
                table: "TripLogs",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Manual",
                table: "TripLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Responder",
                table: "TripAccidents",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments2",
                table: "TripLogs");

            migrationBuilder.DropColumn(
                name: "GetOffDateTime2",
                table: "TripLogs");

            migrationBuilder.DropColumn(
                name: "Latitude2",
                table: "TripLogs");

            migrationBuilder.DropColumn(
                name: "Location2",
                table: "TripLogs");

            migrationBuilder.DropColumn(
                name: "Longitude2",
                table: "TripLogs");

            migrationBuilder.DropColumn(
                name: "Manual",
                table: "TripLogs");

            migrationBuilder.DropColumn(
                name: "Responder",
                table: "TripAccidents");

            migrationBuilder.RenameColumn(
                name: "UID",
                table: "TripLogs",
                newName: "OnOrOff");

            migrationBuilder.RenameColumn(
                name: "Manual2",
                table: "TripLogs",
                newName: "Missing");

            migrationBuilder.RenameColumn(
                name: "GetOnDateTime",
                table: "TripLogs",
                newName: "SwipeDateTime");
        }
    }
}
