using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RulesModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "have excactly one objective");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 20,
                column: "Description",
                value: "have not more than 5 key results");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 21,
                column: "Description",
                value: "have at least 1 key result");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 22,
                column: "Description",
                value: "have at least 3 key results");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "excactly one objective");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 20,
                column: "Description",
                value: "not more than 5 key results");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 21,
                column: "Description",
                value: "at least 1 key result");

            migrationBuilder.UpdateData(
                table: "OkrRules",
                keyColumn: "Id",
                keyValue: 22,
                column: "Description",
                value: "at least 3 key results");
        }
    }
}
