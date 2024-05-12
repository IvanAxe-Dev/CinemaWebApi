using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TicketTable_SessionTable_CinemaHallTable_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_CinemaHalls_CinemaHallId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "CinemaHallId",
                table: "Seats",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_CinemaHallId",
                table: "Seats",
                newName: "IX_Seats_SessionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedAt",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Sessions_SessionId",
                table: "Seats",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Sessions_SessionId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "BookedAt",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Seats",
                newName: "CinemaHallId");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_SessionId",
                table: "Seats",
                newName: "IX_Seats_CinemaHallId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_CinemaHalls_CinemaHallId",
                table: "Seats",
                column: "CinemaHallId",
                principalTable: "CinemaHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
