using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CinemaHallTable_Count : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeatsCount",
                table: "CinemaHalls",
                newName: "RowsCount");

            migrationBuilder.AddColumn<int>(
                name: "NumbersCount",
                table: "CinemaHalls",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumbersCount",
                table: "CinemaHalls");

            migrationBuilder.RenameColumn(
                name: "RowsCount",
                table: "CinemaHalls",
                newName: "SeatsCount");
        }
    }
}
