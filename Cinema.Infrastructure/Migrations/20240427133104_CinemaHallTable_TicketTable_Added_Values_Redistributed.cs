using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CinemaHallTable_TicketTable_Added_Values_Redistributed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CinemaHall",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Graphics",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Privilege",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaHallId",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Sessions",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "Sessions",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeRestriction",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ratings",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalEndDate",
                table: "Movies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalStartDate",
                table: "Movies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CinemaHalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Graphics = table.Column<int>(type: "int", nullable: true),
                    Privilege = table.Column<int>(type: "int", nullable: true),
                    Seats = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatRow = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CinemaHallId",
                table: "Sessions",
                column: "CinemaHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SessionId",
                table: "Tickets",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_CinemaHalls_CinemaHallId",
                table: "Sessions",
                column: "CinemaHallId",
                principalTable: "CinemaHalls",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_CinemaHalls_CinemaHallId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "CinemaHalls");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CinemaHallId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CinemaHallId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "AgeRestriction",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RentalEndDate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RentalStartDate",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "CinemaHall",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Sessions",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Graphics",
                table: "Sessions",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Privilege",
                table: "Sessions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Sessions",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Movies",
                type: "float",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
