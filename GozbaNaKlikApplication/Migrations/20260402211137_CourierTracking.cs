using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GozbaNaKlikApplication.Migrations
{
    /// <inheritdoc />
    public partial class CourierTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentLatitude",
                table: "Couriers",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLongitude",
                table: "Couriers",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLatitude",
                table: "Couriers");

            migrationBuilder.DropColumn(
                name: "CurrentLongitude",
                table: "Couriers");
        }
    }
}
