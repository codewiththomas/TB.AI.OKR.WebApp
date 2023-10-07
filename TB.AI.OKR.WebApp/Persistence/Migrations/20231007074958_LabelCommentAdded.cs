using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LabelCommentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "OkrSetLabels",
                newName: "ValueType");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "OkrSetElementLabels",
                newName: "ValueType");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "OkrSetLabels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelName",
                table: "OkrSetLabels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "OkrSetElementLabels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelName",
                table: "OkrSetElementLabels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "OkrSetLabels");

            migrationBuilder.DropColumn(
                name: "LabelName",
                table: "OkrSetLabels");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "OkrSetElementLabels");

            migrationBuilder.DropColumn(
                name: "LabelName",
                table: "OkrSetElementLabels");

            migrationBuilder.RenameColumn(
                name: "ValueType",
                table: "OkrSetLabels",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ValueType",
                table: "OkrSetElementLabels",
                newName: "Title");
        }
    }
}
