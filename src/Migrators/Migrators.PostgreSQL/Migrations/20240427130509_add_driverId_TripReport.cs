using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class add_driverId_TripReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "TripReports",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripReports_DriverId",
                table: "TripReports",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripReports_AspNetUsers_DriverId",
                table: "TripReports",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripReports_AspNetUsers_DriverId",
                table: "TripReports");

            migrationBuilder.DropIndex(
                name: "IX_TripReports_DriverId",
                table: "TripReports");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "TripReports");
        }
    }
}
