using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    IsSpecificRating = table.Column<double>(type: "REAL", nullable: false),
                    IsMeasurableRating = table.Column<double>(type: "REAL", nullable: false),
                    IsArchievableRating = table.Column<double>(type: "REAL", nullable: false),
                    IsRelevantRating = table.Column<double>(type: "REAL", nullable: false),
                    IsTimeBoundedRating = table.Column<double>(type: "REAL", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OkrRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4096, nullable: false),
                    Prompt = table.Column<string>(type: "TEXT", nullable: true),
                    Scope = table.Column<int>(type: "INTEGER", nullable: false),
                    Severity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReferenceSourceType = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    SubTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Authors = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<string>(type: "TEXT", nullable: true),
                    URL = table.Column<string>(type: "TEXT", nullable: true),
                    DOI = table.Column<string>(type: "TEXT", nullable: true),
                    Publisher = table.Column<string>(type: "TEXT", nullable: true)
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
                    ObjectiveId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyResults_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
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
                    SampleOkrId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Objectives_SampleOkrId",
                        column: x => x.SampleOkrId,
                        principalTable: "Objectives",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OkrRuleReferenceSource",
                columns: table => new
                {
                    OkrRulesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReferenceSourcesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrRuleReferenceSource", x => new { x.OkrRulesId, x.ReferenceSourcesId });
                    table.ForeignKey(
                        name: "FK_OkrRuleReferenceSource_OkrRules_OkrRulesId",
                        column: x => x.OkrRulesId,
                        principalTable: "OkrRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkrRuleReferenceSource_ReferenceSources_ReferenceSourcesId",
                        column: x => x.ReferenceSourcesId,
                        principalTable: "ReferenceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OkrRules",
                columns: new[] { "Id", "Description", "Prompt", "Scope", "Severity" },
                values: new object[,]
                {
                    { 19, "excactly one objective", null, 1, 2 },
                    { 20, "not more than 5 key results", null, 1, 1 },
                    { 21, "at least 1 key result", null, 1, 2 },
                    { 22, "at least 3 key results", null, 1, 1 },
                    { 23, "can be abbreviated with O", null, 2, 0 },
                    { 24, "describes the \"What\"", null, 2, 0 },
                    { 25, "expresses goals or intends", null, 2, 0 },
                    { 26, "be aggressive, yet realistic", null, 2, 1 },
                    { 27, "be tangible, objective, and unambigous", null, 2, 1 },
                    { 28, "be obvious to a rational observer whether an objective has been achieved", null, 2, 1 },
                    { 29, "provide clear value to the company when successful achieved", null, 2, 2 },
                    { 30, "can be abbreviated with KR", null, 3, 0 },
                    { 31, "describes the \"How\"", null, 3, 0 },
                    { 32, "express measurable outcome", null, 3, 0 },
                    { 33, "express an outcome instead an output", null, 3, 1 },
                    { 34, "describe outcome, not activities (if words like consult, help, analyze, or participate are included, it describes activities)", null, 3, 1 },
                    { 35, "measurable and verifiable", null, 3, 1 },
                    { 36, "be difficult but not impossible to achieve", null, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_ObjectiveId",
                table: "KeyResults",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrRuleReferenceSource_ReferenceSourcesId",
                table: "OkrRuleReferenceSource",
                column: "ReferenceSourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SampleOkrId",
                table: "Reviews",
                column: "SampleOkrId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyResults");

            migrationBuilder.DropTable(
                name: "OkrRuleReferenceSource");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "OkrRules");

            migrationBuilder.DropTable(
                name: "ReferenceSources");

            migrationBuilder.DropTable(
                name: "Objectives");
        }
    }
}
