using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GozbaNaKlikApplication.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, "aleksandarpopov@gmail.com", "Aleksandar", "admin1", 2, "Popov", "Admin1" },
                    { 2, "nikolapopovski@gmail.com", "Nikola", "admin2", 2, "Popovski", "Admin2" },
                    { 3, "petarnikolic@gmail.com", "Petar", "admin3", 2, "Nikolic", "Admin3" }
                });

            migrationBuilder.InsertData(
                table: "Administrators",
                column: "UserId",
                values: new object[]
                {
                    1,
                    2,
                    3
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
