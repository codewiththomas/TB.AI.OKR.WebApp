using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TB.AI.OKR.Core.Application;
using TB.AI.OKR.Core.Domain;
using TB.AI.OKR.Infrastructure.Persistence.Contexts;
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


var labelProviders = new List<LabelProviders>
{
    LabelProviders.Annotator,
    LabelProviders.OpenAI_GPT_35_Turbo,
    LabelProviders.ML,
    LabelProviders.GPT4All_Hermes_LLaMA2
};


#region Check deterministic behavior

foreach (var labelProvider in labelProviders.Where(x => !x.Equals(LabelProviders.Annotator)))
{
    var randomElementsCount = 10;
    var results = new List<double>();

    for (int i = 0; i < randomElementsCount; i++)
    {
        var randomElement = await applicationDbContext.OkrSetElements
            .OrderBy(x => EF.Functions.Random())
            .Take(1)
            .FirstAsync();

        var randomElementType = randomElement.Type;

        var scopeForRandomRule = randomElement.Type switch
        {
            "objective" => OkrRuleScopes.Objective,
            "keyresult" => OkrRuleScopes.KeyResult,
            _ => OkrRuleScopes.OkrSet
        };

        var randomRule = await applicationDbContext.OkrRules
            .Where(x => x.Scope == scopeForRandomRule)
            .Where(x => x.Severity >= OkrRuleSeverities.Should)
            .OrderBy(x => EF.Functions.Random())
            .Take(1)
            .FirstAsync();

        Console.WriteLine($"{randomElement.Id}\t{randomElement.Type}\t{randomElement.Text.Substring(0, 10)}...");
        Console.WriteLine($"\tLabel rule_{randomRule.Id}");

        var retries = 100;
        var deterministicBehaviorChecker = new DeterministicBehaviorChecker(config, labelProvider);
        var determisticCheckResult = await deterministicBehaviorChecker
            .CheckDetermisticBehavior(randomElement, randomRule, retries);

        Console.WriteLine($"Mean for {labelProvider}: {determisticCheckResult}");
    }
    Console.WriteLine("Total Mean for LabelProvider: " + results.Average());
}




return;

#endregion






foreach (var labelProvider  in labelProviders)
{


    var labeler = new Labeler(config, labelProvider);

    var okrSets = await applicationDbContext.OkrSets
        .Include(x => x.OkrSetElements)
        .ToListAsync();


    #region ObviousLabels

    #region Label Objectives Count

    //foreach (var okrSet in okrSets)
    //{
    //    var objectiveCountLabel = labeler.OkrSetLabeler.CreateObjectiveCountLabel(okrSet);

    //    var objectiveCountLabelExists = await applicationDbContext.OkrSetLabels
    //        .Where(x => x.EntityId == okrSet.Id)
    //        .Where(x => x.LabelProvider == objectiveCountLabel.LabelProvider)
    //        .Where(x => x.LabelName == objectiveCountLabel.LabelName)
    //        .AnyAsync();

    //    if (objectiveCountLabelExists)
    //    {
    //        applicationDbContext.OkrSetLabels.Update(objectiveCountLabel);
    //    }
    //    else
    //    {
    //        await applicationDbContext.OkrSetLabels.AddAsync(objectiveCountLabel);
    //    }    
    //}

    //await applicationDbContext.SaveChangesAsync();

    #endregion


    #region Label Key Results Count

    //foreach (var okrSet in okrSets)
    //{
    //    var keyResultCountLabel = labeler.OkrSetLabeler.CreateKeyResultsCountLabel(okrSet);

    //    var keyResultCountLabelExists = await applicationDbContext.OkrSetLabels
    //        .Where(x => x.EntityId == okrSet.Id)
    //        .Where(x => x.LabelProvider == keyResultCountLabel.LabelProvider)
    //        .Where(x => x.LabelName == keyResultCountLabel.LabelName)
    //        .AnyAsync();

    //    if (keyResultCountLabelExists)
    //    {
    //        applicationDbContext.OkrSetLabels.Update(keyResultCountLabel);
    //    }
    //    else
    //    {
    //        await applicationDbContext.OkrSetLabels.AddAsync(keyResultCountLabel);
    //    }    
    //}

    //await applicationDbContext.SaveChangesAsync();

    #endregion


    #region Label Readability of each Objective

    //foreach (var okrSet in okrSets)
    //{
    //    ///* Readability of objective */
    //    foreach (var objective in okrSet.OkrSetElements.Where(x => x.Type == "objective").OrderBy(x => x.Id))
    //    {
    //        //labeler.ObjectiveLabeler.ElementNumber = elementNumber++;
    //        var label = labeler.ObjectiveLabeler.CreateReadabilityLabel(objective);
    //        var labelExists = await applicationDbContext.OkrSetElementLabels
    //            .Where(x => x.EntityId == objective.Id)
    //            .Where(x => x.LabelProvider == label.LabelProvider)
    //            .Where(x => x.LabelName == label.LabelName)
    //            .AnyAsync();
    //        if (labelExists)
    //        {
    //            applicationDbContext.OkrSetElementLabels.Update(label);
    //        }
    //        else
    //        {
    //            await applicationDbContext.OkrSetElementLabels.AddAsync(label);
    //        }
    //        await applicationDbContext.SaveChangesAsync();
    //    }
    //}

    #endregion


    #region Label Readability of each KeyResult

    //foreach (var okrSet in okrSets)
    //{
    //    foreach (var element in okrSet.OkrSetElements.Where(x => x.Type == "keyresult").OrderBy(x => x.Id))
    //    {
    //        //labeler.KeyResultLabeler.ElementNumber = elementNumber++;
    //        var label = labeler.KeyResultLabeler.CreateReadabilityLabel(element);
    //        var labelExists = await applicationDbContext.OkrSetElementLabels
    //            .Where(x => x.EntityId == element.Id)
    //            .Where(x => x.LabelProvider == label.LabelProvider)
    //            .Where(x => x.LabelName == label.LabelName)
    //            .AnyAsync();
    //        if (labelExists)
    //        {
    //            applicationDbContext.OkrSetElementLabels.Update(label);
    //        }
    //        else
    //        {
    //            await applicationDbContext.OkrSetElementLabels.AddAsync(label);
    //        }
    //        await applicationDbContext.SaveChangesAsync();
    //    }
    //}

    #endregion


    #endregion


    #region Create labels for all rules with Open AI

    var ruleScopes = await applicationDbContext.OkrRules
        .Select(x => x.Scope)
        .Distinct()
        .ToListAsync();

    /* Iterate through all scopes (OKR set, Objective, Key Result) */
    foreach (var ruleScope in ruleScopes)
    {
        Console.WriteLine(ruleScope);
        var rulesInScope = await applicationDbContext.OkrRules
            .Where(x => x.IsActive)
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
                        .Where(x => x.LabelName.Equals(labelName))
                        .Where(x => x.EntityId == okrSet.Id)
                        .Where(x => x.LabelProvider == (labelProvider.GetDescription() ?? labelProvider.ToString()))
                        .AnyAsync();

                    if (existingLabel)
                    {
                        continue;
                    }
                    var labelEntity = await labeler.OkrSetLabeler.CreateLabelByRule(okrSet, okrRule);

                    await applicationDbContext.OkrSetLabels.AddAsync(labelEntity);
                    await applicationDbContext.SaveChangesAsync();
                }
            }

            /* OKR elements */
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
                        .Where(x => x.LabelName.Equals(labelName))
                        .Where(x => x.EntityId == okrSetElement.Id)
                        .Where(x => x.LabelProvider == (labelProvider.GetDescription() ?? labelProvider.ToString()))
                        .AnyAsync();

                    if (existingLabel)
                    {
                        continue;
                    }

                    if (ruleScope == OkrRuleScopes.Objective)
                    {
                        var labelEntity = await labeler.ObjectiveLabeler.CreateLabelByRule(okrSetElement, okrRule);
                        await applicationDbContext.OkrSetElementLabels.AddAsync(labelEntity);
                    }
                    else
                    {
                        var labelEntity = await labeler.KeyResultLabeler.CreateLabelByRule(okrSetElement, okrRule);
                        await applicationDbContext.OkrSetElementLabels.AddAsync(labelEntity);
                    }

                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }
    }

    #endregion
}








