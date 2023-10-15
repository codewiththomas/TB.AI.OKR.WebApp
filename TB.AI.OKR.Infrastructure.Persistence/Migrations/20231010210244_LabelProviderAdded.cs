using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.AI.OKR.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LabelProviderAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrSetLabels",
                table: "OkrSetLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrSetElementLabels",
                table: "OkrSetElementLabels");

            migrationBuilder.AddColumn<string>(
                name: "LabelProvider",
                table: "OkrSetLabels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LabelProvider",
                table: "OkrSetElementLabels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrSetLabels",
                table: "OkrSetLabels",
                columns: new[] { "EntityId", "LabelProvider", "LabelName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrSetElementLabels",
                table: "OkrSetElementLabels",
                columns: new[] { "EntityId", "LabelProvider", "LabelName" });
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

            migrationBuilder.DropColumn(
                name: "LabelProvider",
                table: "OkrSetLabels");

            migrationBuilder.DropColumn(
                name: "LabelProvider",
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
    }
}
