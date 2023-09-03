using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OkrRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4096, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Scope = table.Column<int>(type: "INTEGER", nullable: false),
                    Severity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Okrs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Objective = table.Column<string>(type: "TEXT", nullable: false),
                    IsSpecificRating = table.Column<double>(type: "REAL", nullable: false),
                    IsMeasurableRating = table.Column<double>(type: "REAL", nullable: false),
                    IsArchievableRating = table.Column<double>(type: "REAL", nullable: false),
                    IsRelevantRating = table.Column<double>(type: "REAL", nullable: false),
                    IsTimeBoundedRating = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okrs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReferenceSymbol = table.Column<string>(type: "TEXT", nullable: false),
                    ReferenceText = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OkrId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyResults_Okrs_OkrId",
                        column: x => x.OkrId,
                        principalTable: "Okrs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReviewTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Provider = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: true),
                    OkrId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Okrs_OkrId",
                        column: x => x.OkrId,
                        principalTable: "Okrs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OkrReferenceSource",
                columns: table => new
                {
                    OkrsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReferencesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrReferenceSource", x => new { x.OkrsId, x.ReferencesId });
                    table.ForeignKey(
                        name: "FK_OkrReferenceSource_Okrs_OkrsId",
                        column: x => x.OkrsId,
                        principalTable: "Okrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkrReferenceSource_ReferenceSources_ReferencesId",
                        column: x => x.ReferencesId,
                        principalTable: "ReferenceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OkrRuleReferenceSource",
                columns: table => new
                {
                    OkrRulesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReferencesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrRuleReferenceSource", x => new { x.OkrRulesId, x.ReferencesId });
                    table.ForeignKey(
                        name: "FK_OkrRuleReferenceSource_OkrRules_OkrRulesId",
                        column: x => x.OkrRulesId,
                        principalTable: "OkrRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkrRuleReferenceSource_ReferenceSources_ReferencesId",
                        column: x => x.ReferencesId,
                        principalTable: "ReferenceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OkrRules",
                columns: new[] { "Id", "Description", "IsActive", "Scope", "Severity" },
                values: new object[,]
                {
                    { 19, "have excactly one objective", true, 1, 2 },
                    { 20, "have not more than 5 key results", true, 1, 1 },
                    { 21, "have at least 1 key result", true, 1, 2 },
                    { 22, "have at least 3 key results", true, 1, 1 },
                    { 23, "can be abbreviated with O", true, 2, 0 },
                    { 24, "describes the \"What\"", true, 2, 0 },
                    { 25, "expresses goals or intends", true, 2, 0 },
                    { 26, "be aggressive, yet realistic", true, 2, 1 },
                    { 27, "be tangible, objective, and unambigous", true, 2, 1 },
                    { 28, "be obvious to a rational observer whether an objective has been achieved", true, 2, 1 },
                    { 29, "provide clear value to the company when successful achieved", true, 2, 2 },
                    { 30, "can be abbreviated with KR", true, 3, 0 },
                    { 31, "describes the \"How\"", true, 3, 0 },
                    { 32, "express measurable outcome", true, 3, 0 },
                    { 33, "express an outcome instead an output", true, 3, 1 },
                    { 34, "describe outcome, not activities (if words like consult, help, analyze, or participate are included, it describes activities)", true, 3, 1 },
                    { 35, "measurable and verifiable", true, 3, 1 },
                    { 36, "be difficult but not impossible to achieve", true, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_OkrId",
                table: "KeyResults",
                column: "OkrId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrReferenceSource_ReferencesId",
                table: "OkrReferenceSource",
                column: "ReferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrRuleReferenceSource_ReferencesId",
                table: "OkrRuleReferenceSource",
                column: "ReferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OkrId",
                table: "Reviews",
                column: "OkrId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyResults");

            migrationBuilder.DropTable(
                name: "OkrReferenceSource");

            migrationBuilder.DropTable(
                name: "OkrRuleReferenceSource");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "OkrRules");

            migrationBuilder.DropTable(
                name: "ReferenceSources");

            migrationBuilder.DropTable(
                name: "Okrs");
        }
    }
}
