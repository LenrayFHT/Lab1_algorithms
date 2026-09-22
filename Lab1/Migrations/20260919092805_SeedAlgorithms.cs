using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class SeedAlgorithms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Algorithms",
                columns: new[] { "Id", "BigONotation", "Description", "Name" },
                values: new object[,]
                {
                    { 3, "O(1)", "", "Константная функция" },
                    { 4, "O(n)", "", "Сумма элементов" },
                    { 5, "O(n)", "", "Произведение элементов" },
                    { 6, "O(n^2)", "", "Полином (в лоб)" },
                    { 7, "O(n)", "", "Полином (Горнер)" },
                    { 8, "O(n log n)", "", "Встроенная сортировка (Array.Sort)" },
                    { 9, "O(n^3)", "", "Умножение матриц" },
                    { 10, "O(n)", "", "Степень (Наивный)" },
                    { 11, "O(n)", "", "Степень (Рекурсивный)" },
                    { 12, "O(log n)", "", "Степень (Быстрый/Бинарный)" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
