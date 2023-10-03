using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReferencesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Okrs_OkrId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "KeyResults");

            migrationBuilder.DropTable(
                name: "OkrReferenceSource");

            migrationBuilder.DropTable(
                name: "Okrs");

            migrationBuilder.RenameColumn(
                name: "OkrId",
                table: "Reviews",
                newName: "OkrSetId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_OkrId",
                table: "Reviews",
                newName: "IX_Reviews_OkrSetId");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OkrRules",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OkrSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OkrSetElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    OkrSetId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrSetElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OkrSetElements_OkrSets_OkrSetId",
                        column: x => x.OkrSetId,
                        principalTable: "OkrSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OkrSetLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrSetLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OkrSetLabels_OkrSets_EntityId",
                        column: x => x.EntityId,
                        principalTable: "OkrSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OkrSetReferenceSource",
                columns: table => new
                {
                    OkrSetsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReferencesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrSetReferenceSource", x => new { x.OkrSetsId, x.ReferencesId });
                    table.ForeignKey(
                        name: "FK_OkrSetReferenceSource_OkrSets_OkrSetsId",
                        column: x => x.OkrSetsId,
                        principalTable: "OkrSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkrSetReferenceSource_ReferenceSources_ReferencesId",
                        column: x => x.ReferencesId,
                        principalTable: "ReferenceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OkrSetElementLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrSetElementLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OkrSetElementLabels_OkrSetElements_EntityId",
                        column: x => x.EntityId,
                        principalTable: "OkrSetElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 19,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 20,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 21,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 22,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 23,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 24,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 25,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 26,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 27,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 28,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 29,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 30,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 31,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 32,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 33,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 34,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 35,
                column: "Title",
                value: "");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 36,
                column: "Title",
                value: "");

            migrationBuilder.CreateIndex(
                name: "IX_OkrSetElementLabels_EntityId",
                table: "OkrSetElementLabels",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrSetElements_OkrSetId",
                table: "OkrSetElements",
                column: "OkrSetId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrSetLabels_EntityId",
                table: "OkrSetLabels",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrSetReferenceSource_ReferencesId",
                table: "OkrSetReferenceSource",
                column: "ReferencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_OkrSets_OkrSetId",
                table: "Reviews",
                column: "OkrSetId",
                principalTable: "OkrSets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_OkrSets_OkrSetId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "OkrSetElementLabels");

            migrationBuilder.DropTable(
                name: "OkrSetLabels");

            migrationBuilder.DropTable(
                name: "OkrSetReferenceSource");

            migrationBuilder.DropTable(
                name: "OkrSetElements");

            migrationBuilder.DropTable(
                name: "OkrSets");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "OkrRules");

            migrationBuilder.RenameColumn(
                name: "OkrSetId",
                table: "Reviews",
                newName: "OkrId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_OkrSetId",
                table: "Reviews",
                newName: "IX_Reviews_OkrId");

            migrationBuilder.CreateTable(
                name: "Okrs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsArchievableRating = table.Column<double>(type: "REAL", nullable: false),
                    IsMeasurableRating = table.Column<double>(type: "REAL", nullable: false),
                    IsRelevantRating = table.Column<double>(type: "REAL", nullable: false),
                    IsSpecificRating = table.Column<double>(type: "REAL", nullable: false),
                    IsTimeBoundedRating = table.Column<double>(type: "REAL", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    Objective = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okrs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OkrId = table.Column<int>(type: "INTEGER", nullable: true),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_OkrId",
                table: "KeyResults",
                column: "OkrId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrReferenceSource_ReferencesId",
                table: "OkrReferenceSource",
                column: "ReferencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Okrs_OkrId",
                table: "Reviews",
                column: "OkrId",
                principalTable: "Okrs",
                principalColumn: "Id");
        }
    }
}
