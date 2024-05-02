using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class add_driver_Itinerary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itineraries_Pilots_PilotId",
                table: "Itineraries");

            migrationBuilder.AlterColumn<int>(
                name: "PilotId",
                table: "Itineraries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "Itineraries",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Itineraries_DriverId",
                table: "Itineraries",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Itineraries_AspNetUsers_DriverId",
                table: "Itineraries",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Itineraries_Pilots_PilotId",
                table: "Itineraries",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itineraries_AspNetUsers_DriverId",
                table: "Itineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Itineraries_Pilots_PilotId",
                table: "Itineraries");

            migrationBuilder.DropIndex(
                name: "IX_Itineraries_DriverId",
                table: "Itineraries");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Itineraries");

            migrationBuilder.AlterColumn<int>(
                name: "PilotId",
                table: "Itineraries",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Itineraries_Pilots_PilotId",
                table: "Itineraries",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
