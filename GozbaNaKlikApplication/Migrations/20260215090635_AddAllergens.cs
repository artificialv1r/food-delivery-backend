using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GozbaNaKlikApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddAllergens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAllergens",
                columns: table => new
                {
                    CustomerAllergensId = table.Column<int>(type: "integer", nullable: false),
                    CustomerProfileUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAllergens", x => new { x.CustomerAllergensId, x.CustomerProfileUserId });
                    table.ForeignKey(
                        name: "FK_CustomerAllergens_Allergens_CustomerAllergensId",
                        column: x => x.CustomerAllergensId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAllergens_Customers_CustomerProfileUserId",
                        column: x => x.CustomerProfileUserId,
                        principalTable: "Customers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealAllergens",
                columns: table => new
                {
                    MealAllergensId = table.Column<int>(type: "integer", nullable: false),
                    MealId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealAllergens", x => new { x.MealAllergensId, x.MealId });
                    table.ForeignKey(
                        name: "FK_MealAllergens_Allergens_MealAllergensId",
                        column: x => x.MealAllergensId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealAllergens_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Gluten" },
                    { 2, "Lactose" },
                    { 3, "Peanuts" },
                    { 4, "Soy" },
                    { 5, "Eggs" },
                    { 6, "Fish" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAllergens_CustomerProfileUserId",
                table: "CustomerAllergens",
                column: "CustomerProfileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "CustomerAllergens");

            migrationBuilder.DropTable(
                name: "MealAllergens");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Restaurants_RestaurantId",
                table: "Meals",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
