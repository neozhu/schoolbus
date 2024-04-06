using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.SqLite.Migrations
{
    /// <inheritdoc />
    public partial class ItineraryId_Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItineraryId",
                table: "Students",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ItineraryId",
                table: "Students",
                column: "ItineraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Itineraries_ItineraryId",
                table: "Students",
                column: "ItineraryId",
                principalTable: "Itineraries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Itineraries_ItineraryId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ItineraryId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ItineraryId",
                table: "Students");
        }
    }
}
