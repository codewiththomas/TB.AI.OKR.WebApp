using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Seeds;

public static class ReferenceSourceSeeds
{

    private static int nextOkrRuleId = 1;

    public static void SeedReferenceSources(this ModelBuilder modelBuilder)
    {
        int referenceSourceId = 1;

        modelBuilder.Entity<ReferenceSource>().HasData(
            
            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Niven & Lamorte, 2016",
                ReferenceText = "Niven, P. R., Lamorte, B. (2016). Objectives and Key Results: Driving Focus, Alignment, and Engagement with OKRs. Wiley.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Lamorte, 2022",
                ReferenceText = "Lamorte, B. (2022). The OKRs Fiel Book. Wiley.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Mello, 2019",
                ReferenceText = "Mello, F. S. H. (2019). OKRs: From Mission to Metrics. Qulture.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Doerr, 2018",
                ReferenceText = "Doerr, J. (2018). Measure What Matters. Penguin.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Wodtke, 2021",
                ReferenceText = "Wodtke, C. (2021). Radical Focus. Second Edition. Cucina Media.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Hellesoe & Mewes, 2020",
                ReferenceText = "Hellesoe, N., Mewes, S. (2020). OKRs at the Center. Sense & Respond Press.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Lobacher & Jacob (2020)",
                ReferenceText = "Lobacher, P., Jacob, C. (2020). Objectives & Key Results: Das agile Betriebssystem für moderne Organisationen. die.agilen.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Kudernatsch, 2021",
                ReferenceText = "Kudernatsch, D. (2021). Objectives and Key Results: Die Grundlagen der agilen Managementmethode OKR. Haufe.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Lange, 2022",
                ReferenceText = "Lange, C. (2022). OKR in der Praxis. Business Village.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Kudernatsch, 2021",
                ReferenceText = "Kudernatsch, D. (2021). Objectives and Key Results: Die Grundlagen der agilen Managementmethode OKR. Haufe.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Obogeanu-Hempel & Steiner, 2023",
                ReferenceText = "Obogeanu-Hempel, E. M., Steiner, A. D. (2023). OKR - Objectives & Key Results. Gabal.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Mooncamp, 2023",
                ReferenceText = "Mooncamp (2023). OKR Beispiele. https://mooncamp.com/de/okr-beispiele. Visited 03/10/2023.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Adobe, 2022",
                ReferenceText = "Adobe Communications Team (2022). OKR Examples. https://business.adobe.com/blog/basics/okr-examples. Visited 03/10/2023.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Quantive, 2023",
                ReferenceText = "Quantive (2023). 30+ Real OKR Examples for Different Teams. https://quantive.com/resources/articles/okr-examples. Visited 03/10/2023.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Bahlinger, 2023",
                ReferenceText = "Bahlinger, M. (2023). OKR examples for different departments. https://www.workpath.com/magazine/okr-examples. WorkPath. Visited 06/09/2023.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Golightly, 2023",
                ReferenceText = "Golightly, E. (2023). 60+ OKR Examples - How To Write Effective OKRs 2023. https://clickup.com/blog/okr-examples. ClickUp. Visited 03/10/2023.",
            },

            new ReferenceSource
            {
                Id = referenceSourceId++,
                ReferenceSymbol = "Hall, 2022",
                ReferenceText = "Hall, S. L. (2022). How to Write Effective OKRs - Plus Examples. https://lattice.com/library/how-to-write-effective-okrs-plus-examples. Lattice. Visited 03/10/2023.",
            }

        );
    }

}
