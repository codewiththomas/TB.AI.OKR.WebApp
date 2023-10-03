using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyReferencesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OkrSetElements_OkrSets_OkrSetId",
                table: "OkrSetElements");

            migrationBuilder.AlterColumn<int>(
                name: "OkrSetId",
                table: "OkrSetElements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OkrSetElements_OkrSets_OkrSetId",
                table: "OkrSetElements",
                column: "OkrSetId",
                principalTable: "OkrSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OkrSetElements_OkrSets_OkrSetId",
                table: "OkrSetElements");

            migrationBuilder.AlterColumn<int>(
                name: "OkrSetId",
                table: "OkrSetElements",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_OkrSetElements_OkrSets_OkrSetId",
                table: "OkrSetElements",
                column: "OkrSetId",
                principalTable: "OkrSets",
                principalColumn: "Id");
        }
    }
}
