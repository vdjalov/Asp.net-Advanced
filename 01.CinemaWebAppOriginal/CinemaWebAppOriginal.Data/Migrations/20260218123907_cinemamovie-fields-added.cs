using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaWebAppOriginal.Data.Migrations
{
    /// <inheritdoc />
    public partial class cinemamoviefieldsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableTickets",
                table: "CinemasMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CinemasMovies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableTickets",
                table: "CinemasMovies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CinemasMovies");
        }
    }
}
