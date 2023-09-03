using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RulesIsActiveStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prompt",
                table: "OkrRules");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OkrRules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 19,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 20,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 21,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 22,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 23,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 24,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 25,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 26,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 27,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 28,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 29,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 30,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 31,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 32,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 33,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 34,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 35,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 36,
                column: "IsActive",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OkrRules");

            migrationBuilder.AddColumn<string>(
                name: "Prompt",
                table: "OkrRules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 19,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 20,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 21,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 22,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 23,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 24,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 25,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 26,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 27,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 28,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 29,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 30,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 31,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 32,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 33,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 34,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 35,
                column: "Prompt",
                value: null);

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 36,
                column: "Prompt",
                value: null);
        }
    }
}
