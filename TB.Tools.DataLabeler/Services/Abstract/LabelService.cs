using Microsoft.Extensions.Configuration;
using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.OpenAI.ApiClient.Abstract.Contracts.Chat;

namespace TB.Tools.DataLabeler.Services.Abstract;

public abstract class LabelService<T>
{
    protected readonly IConfiguration _configuration;

    public LabelService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public abstract Task<Label<T>> CreateLabelByRule(T entity, OkrRule rule, bool showConsoleOutput = true);


    protected string GetLabelName(OkrRule okrRule)
        => $"rule_{okrRule.Id}";


    protected FunctionDefinition GetClassificationFunctionDefinition()
    {
        var functionDefinition = new FunctionDefinition
        {
            Name = "store_classification",
            Description = "Stores the classification value for an OKR set.",
            Parameters = new
            {
                type = "object",
                properties = new
                {
                    rule_applies = new
                    {
                        type = "string",
                        @enum = new string[] { "YES", "NO", "null" },
                        description = "Decission, if the rule applies to the given OKR set."
                    },
                    explanation = new
                    {
                        type = "string",
                        description = "Short explanation for the decission you made."
                    }
                },
                required = new[] { "rule_applies", "explanation" }
            }
        };
        return functionDefinition;
    }



}
