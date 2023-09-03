using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Seeds;

public static class ApplicationDbContextSeeds
{
    private static int nextOkrRuleId = 1;

    public static void SeedRules(this ModelBuilder modelBuilder)
    {
        SeedRulesForOkrScope(modelBuilder);
        SeedRulesForObjectiveScope(modelBuilder);
        SeedRulesForKeyResultScope(modelBuilder);
    }

    private static void SeedRulesForOkrScope(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OkrRule>().HasData(

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "have excactly one objective",
                Scope = OkrRuleScopes.OkrSet,
                Severity = OkrRuleSeverities.Must,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "have not more than 5 key results",
                Scope = OkrRuleScopes.OkrSet,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "have at least 1 key result",
                Scope = OkrRuleScopes.OkrSet,
                Severity = OkrRuleSeverities.Must,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "have at least 3 key results",
                Scope = OkrRuleScopes.OkrSet,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            }
        );
    }


    private static void SeedRulesForObjectiveScope(ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<OkrRule>().HasData(

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "can be abbreviated with O",
                Scope = OkrRuleScopes.Objective,
                Severity = OkrRuleSeverities.Info,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "describes the \"What\"",
                Scope = OkrRuleScopes.Objective,
                Severity = OkrRuleSeverities.Info,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "expresses goals or intends",
                Scope = OkrRuleScopes.Objective,
                Severity = OkrRuleSeverities.Info,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "be aggressive, yet realistic",
                Scope = OkrRuleScopes.Objective,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "be tangible, objective, and unambigous",
                Scope = OkrRuleScopes.Objective,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "be obvious to a rational observer whether an objective has been achieved",
                Scope = OkrRuleScopes.Objective,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "provide clear value to the company when successful achieved",
                Scope = OkrRuleScopes.Objective,
                Severity = OkrRuleSeverities.Must,
                IsActive = true
            }
        );
    }


    private static void SeedRulesForKeyResultScope(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OkrRule>().HasData(

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "can be abbreviated with KR",
                Scope = OkrRuleScopes.KeyResult,
                Severity = OkrRuleSeverities.Info,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "describes the \"How\"",
                Scope = OkrRuleScopes.KeyResult,
                Severity = OkrRuleSeverities.Info,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "express measurable outcome",
                Scope = OkrRuleScopes.KeyResult,
                Severity = OkrRuleSeverities.Info,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "express an outcome instead an output",
                Scope = OkrRuleScopes.KeyResult,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "describe outcome, not activities (if words like consult, help, analyze, or participate are included, it describes activities)",
                Scope = OkrRuleScopes.KeyResult,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "measurable and verifiable",
                Scope = OkrRuleScopes.KeyResult,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            },

            new OkrRule
            {
                Id = nextOkrRuleId++,
                Description = "be difficult but not impossible to achieve",
                Scope = OkrRuleScopes.KeyResult,
                Severity = OkrRuleSeverities.Should,
                IsActive = true
            }

        );
    }

}
