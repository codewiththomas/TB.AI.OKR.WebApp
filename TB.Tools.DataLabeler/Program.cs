using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using TB.AI.OKR.WebApp;
using TB.AI.OKR.WebApp.Extensions;
using TB.AI.OKR.WebApp.Persistence.Contexts;
using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.OpenAI.ApiClient;
using TB.OpenAI.ApiClient.Abstract.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Chat;
using TB.Tools.DataLabeler;


#region appsettings.json and secrets.json

var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

IConfiguration config = builder.Build();

#endregion


#region Init DbContext

using var applicationDbContext = new ApplicationDbContext();

#endregion


var ruleScopes = await applicationDbContext.OkrRules
    .Select(x => x.Scope)
    .Distinct()
    .ToListAsync();

/* Iterate through all scopes (OKR set, Objective, Key Result) */
foreach (var ruleScope in ruleScopes)
{
    Console.WriteLine(ruleScope);
    var rulesInScope = await applicationDbContext.OkrRules
        .Where(x => x.Scope.Equals(ruleScope))
        .ToListAsync();

    /* Iterate through all rules within the scope. Severity must be "should" or higher. */
    foreach (var okrRule in rulesInScope.Where(x => x.Severity >= OkrRuleSeverities.Should))
    {
        var labelName = "rule_" + okrRule.Id.ToString();

        /* OKR sets */
        if (ruleScope.Equals(OkrRuleScopes.OkrSet))
        {
            var okrSetsToLabel = await applicationDbContext.OkrSets
                .Include(x => x.OkrSetElements)
                .ToListAsync();

            foreach (var okrSet in okrSetsToLabel)
            {
                /* Check, if okrSet was labeled already */
                var existingLabel = await applicationDbContext.OkrSetLabels
                    .Where(x => x.LabelName.Equals(labelName) && x.EntityId == okrSet.Id)
                    .AnyAsync();

                if (existingLabel)
                {
                    continue;
                }

                var labeler = new Labeler(config);
                var labelEntity = await labeler.LabelOkrSet(okrSet, okrRule);

                await applicationDbContext.OkrSetLabels.AddAsync(labelEntity);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        else 
        {
            var okrSetElementType = ruleScope switch
            {
                OkrRuleScopes.KeyResult => "keyresult",
                OkrRuleScopes.Objective => "objective",
                _ => null
            };

            if (okrSetElementType is null)
            {
                continue;
            }

            var okrSetElementsToLabel = await applicationDbContext.OkrSetElements
                .Where(x => x.Type == okrSetElementType)
                .ToListAsync();

            foreach (var okrSetElement in okrSetElementsToLabel)
            {
                var existingLabel = await applicationDbContext.OkrSetElementLabels
                    .Where(x => x.LabelName.Equals(labelName) && x.EntityId == okrSetElement.Id)
                    .AnyAsync();

                if (existingLabel)
                {
                    continue;
                }

                var labeler = new Labeler(config);
                var labelEntity = await labeler.LabelOkrSetElement(okrSetElement, okrRule);

                await applicationDbContext.OkrSetElementLabels.AddAsync(labelEntity);
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }




}

