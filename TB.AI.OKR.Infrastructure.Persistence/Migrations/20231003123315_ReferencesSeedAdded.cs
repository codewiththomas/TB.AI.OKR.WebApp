using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TB.AI.OKR.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReferencesSeedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ReferenceSources",
                columns: new[] { "Id", "ReferenceSymbol", "ReferenceText" },
                values: new object[,]
                {
                    { 1, "Niven & Lamorte, 2016", "Niven, P. R., Lamorte, B. (2016). Objectives and Key Results: Driving Focus, Alignment, and Engagement with OKRs. Wiley." },
                    { 2, "Lamorte, 2022", "Lamorte, B. (2022). The OKRs Fiel Book. Wiley." },
                    { 3, "Mello, 2019", "Mello, F. S. H. (2019). OKRs: From Mission to Metrics. Qulture." },
                    { 4, "Doerr, 2018", "Doerr, J. (2018). Measure What Matters. Penguin." },
                    { 5, "Wodtke, 2021", "Wodtke, C. (2021). Radical Focus. Second Edition. Cucina Media." },
                    { 6, "Hellesoe & Mewes, 2020", "Hellesoe, N., Mewes, S. (2020). OKRs at the Center. Sense & Respond Press." },
                    { 7, "Lobacher & Jacob (2020)", "Lobacher, P., Jacob, C. (2020). Objectives & Key Results: Das agile Betriebssystem für moderne Organisationen. die.agilen." },
                    { 8, "Kudernatsch, 2021", "Kudernatsch, D. (2021). Objectives and Key Results: Die Grundlagen der agilen Managementmethode OKR. Haufe." },
                    { 9, "Lange, 2022", "Lange, C. (2022). OKR in der Praxis. Business Village." },
                    { 10, "Kudernatsch, 2021", "Kudernatsch, D. (2021). Objectives and Key Results: Die Grundlagen der agilen Managementmethode OKR. Haufe." },
                    { 11, "Obogeanu-Hempel & Steiner, 2023", "Obogeanu-Hempel, E. M., Steiner, A. D. (2023). OKR - Objectives & Key Results. Gabal." },
                    { 12, "Mooncamp, 2023", "Mooncamp (2023). OKR Beispiele. https://mooncamp.com/de/okr-beispiele. Visited 03/10/2023." },
                    { 13, "Adobe, 2022", "Adobe Communications Team (2022). OKR Examples. https://business.adobe.com/blog/basics/okr-examples. Visited 03/10/2023." },
                    { 14, "Quantive, 2023", "Quantive (2023). 30+ Real OKR Examples for Different Teams. https://quantive.com/resources/articles/okr-examples. Visited 03/10/2023." },
                    { 15, "Bahlinger, 2023", "Bahlinger, M. (2023). OKR examples for different departments. https://www.workpath.com/magazine/okr-examples. WorkPath. Visited 06/09/2023." },
                    { 16, "Golightly, 2023", "Golightly, E. (2023). 60+ OKR Examples - How To Write Effective OKRs 2023. https://clickup.com/blog/okr-examples. ClickUp. Visited 03/10/2023." },
                    { 17, "Hall, 2022", "Hall, S. L. (2022). How to Write Effective OKRs - Plus Examples. https://lattice.com/library/how-to-write-effective-okrs-plus-examples. Lattice. Visited 03/10/2023." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ReferenceSources",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
