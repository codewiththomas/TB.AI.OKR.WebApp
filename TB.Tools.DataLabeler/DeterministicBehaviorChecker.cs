using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TB.AI.OKR.Core.Domain;
using TB.AI.OKR.Infrastructure.Persistence.Contexts;

namespace TB.Tools.DataLabeler;

internal class DeterministicBehaviorChecker
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="labelProvider"></param>
    public DeterministicBehaviorChecker(IConfiguration configuration, LabelProviders labelProvider)
    {
        LabelProvider = labelProvider;
        Labeler = new Labeler(configuration, labelProvider);
    }


    public LabelProviders LabelProvider { get; }
    private Labeler Labeler { get; set; }


    /// <summary>
    /// Checks how often the same label was created
    /// </summary>
    /// <param name="element"></param>
    /// <param name="rule"></param>
    /// <param name="retries"></param>
    /// <returns></returns>
    public async Task<double> CheckDetermisticBehavior(OkrSetElement okrSetElement, OkrRule okrRule, int retries = 100)
    {
        using var applicationDbContext = new ApplicationDbContext();

        var okrSetElementType = okrSetElement.Type;
        var okrRuleScope = okrRule.Scope;

        var labelsCountDictionary = new Dictionary<string, int>();

        for (int i = 0; i < retries; i++)
        {
            Label<OkrSetElement> label;

            if (okrRuleScope == OkrRuleScopes.Objective)
            {
                label = await Labeler.ObjectiveLabeler.CreateLabelByRule(okrSetElement, okrRule, false);
            }
            else
            {
                label = await Labeler.KeyResultLabeler.CreateLabelByRule(okrSetElement, okrRule, false);
            }

            if (!labelsCountDictionary.ContainsKey(label.Value))
            {
                labelsCountDictionary.Add(label.Value, 0);
            }

            labelsCountDictionary[label.Value]++;
            await Console.Out.WriteLineAsync($"\ti = {i}\t{label.Value}\t{labelsCountDictionary[label.Value]}");
        }

        var maxValue = labelsCountDictionary.Max(label => label.Value);
        
        return (double)maxValue / retries;
    }



}
