using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Algorithms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BigONotation = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlgorithmId = table.Column<int>(type: "integer", nullable: false),
                    NMax = table.Column<int>(type: "integer", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    RunsPerPoint = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentSessions_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlgorithmId = table.Column<int>(type: "integer", nullable: false),
                    SessionId = table.Column<int>(type: "integer", nullable: true),
                    N = table.Column<int>(type: "integer", nullable: false),
                    RunNumber = table.Column<int>(type: "integer", nullable: false),
                    ElapsedMs = table.Column<double>(type: "double precision", nullable: false),
                    Steps = table.Column<long>(type: "bigint", nullable: false),
                    ExperimentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiments_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiments_ExperimentSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ExperimentSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Algorithms",
                columns: new[] { "Id", "BigONotation", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "O(n^2)", "Простая сортировка обменом", "Сортировка пузырьком" },
                    { 2, "O(n log n)", "Сортировка Хоара (QuickSort)", "Быстрая сортировка" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_AlgorithmId",
                table: "Experiments",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_SessionId",
                table: "Experiments",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentSessions_AlgorithmId",
                table: "ExperimentSessions",
                column: "AlgorithmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "ExperimentSessions");

            migrationBuilder.DropTable(
                name: "Algorithms");
        }
    }
}
