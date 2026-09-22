using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class AddRadixSort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Algorithms",
                columns: new[] { "Id", "BigONotation", "Description", "Name" },
                values: new object[] { 13, "O(d·n)", "Поразрядная сортировка, LSD, основание 10. Несравнительная, устойчивая, O(d·n).", "Radix Sort (LSD, base 10)" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Algorithms",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
