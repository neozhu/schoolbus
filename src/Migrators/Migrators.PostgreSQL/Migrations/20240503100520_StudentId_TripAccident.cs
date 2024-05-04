using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class StudentId_TripAccident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "TripAccidents",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripAccidents_StudentId",
                table: "TripAccidents",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAccidents_Students_StudentId",
                table: "TripAccidents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripAccidents_Students_StudentId",
                table: "TripAccidents");

            migrationBuilder.DropIndex(
                name: "IX_TripAccidents_StudentId",
                table: "TripAccidents");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "TripAccidents");
        }
    }
}
