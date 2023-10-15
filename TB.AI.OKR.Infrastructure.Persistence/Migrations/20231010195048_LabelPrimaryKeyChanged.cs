using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.AI.OKR.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LabelPrimaryKeyChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrSetLabels",
                table: "OkrSetLabels");

            migrationBuilder.DropIndex(
                name: "IX_OkrSetLabels_EntityId",
                table: "OkrSetLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrSetElementLabels",
                table: "OkrSetElementLabels");

            migrationBuilder.DropIndex(
                name: "IX_OkrSetElementLabels_EntityId",
                table: "OkrSetElementLabels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OkrSetLabels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OkrSetElementLabels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrSetLabels",
                table: "OkrSetLabels",
                columns: new[] { "EntityId", "LabelName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrSetElementLabels",
                table: "OkrSetElementLabels",
                columns: new[] { "EntityId", "LabelName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrSetLabels",
                table: "OkrSetLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrSetElementLabels",
                table: "OkrSetElementLabels");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OkrSetLabels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OkrSetElementLabels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrSetLabels",
                table: "OkrSetLabels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrSetElementLabels",
                table: "OkrSetElementLabels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OkrSetLabels_EntityId",
                table: "OkrSetLabels",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrSetElementLabels_EntityId",
                table: "OkrSetElementLabels",
                column: "EntityId");
        }
    }
}
