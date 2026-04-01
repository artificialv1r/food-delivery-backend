using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GozbaNaKlikApplication.Migrations
{
    /// <inheritdoc />
    public partial class CourierMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourierProfileUserId",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Couriers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CourierWorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourierId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourierWorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourierWorkingHours_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourierProfileUserId",
                table: "Orders",
                column: "CourierProfileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourierWorkingHours_CourierId",
                table: "CourierWorkingHours",
                column: "CourierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Couriers_CourierProfileUserId",
                table: "Orders",
                column: "CourierProfileUserId",
                principalTable: "Couriers",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Couriers_CourierProfileUserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CourierWorkingHours");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CourierProfileUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CourierProfileUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Couriers");
        }
    }
}
