using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json;
using TB.AI.OKR.WebApp;
using TB.AI.OKR.WebApp.Extensions;
using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.OpenAI.ApiClient;
using TB.OpenAI.ApiClient.Abstract.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Chat;
using TB.Tools.DataLabeler.Services.Abstract;

namespace TB.Tools.DataLabeler.Services;

public class OkrSetLabelService : LabelService<OkrSet>
{
    
    /// <summary>
    /// Constructor for dependency injection
    /// </summary>
    /// <param name="configuration"></param>
    public OkrSetLabelService(IConfiguration configuration) : base(configuration)
    { }


    /// <summary>
    /// Labels an OKR-set.
    /// </summary>
    /// <param name="okrSet"></param>
    /// <param name="okrRule"></param>
    /// <param name="showConsoleOutput"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public override async Task<Label<OkrSet>> CreateLabelByRule(OkrSet okrSet, OkrRule okrRule, bool showConsoleOutput = true)
    {
        if (okrRule.Scope != OkrRuleScopes.OkrSet)
        {
            throw new ArgumentException("Only rules with scope OKR set may be applied to OKR sets!");
        }

        var openAiService = new OpenAiApiClient(_configuration);

        var chatCompletionMessage = new CreateChatCompletionRequest
        {
            Model = "gpt-3.5-turbo",
            Messages = new List<CreateChatCompletionRequestMessage>
            {
                new CreateChatCompletionRequestMessage
                {
                    Content =
                        "You are a professional data labeler. You have to task to label a dataset of " +
                        "OKR sets (Objectives and Key Results) towards certain rules.",
                    Role = "system"
                },
                new CreateChatCompletionRequestMessage
                {
                    Content =
                        $"This is the OKR set you have to label:\n" +
                        $"**BEGIN** \n{okrSet.ToSetString()}**END**\n" +
                        $"Decide, if the given OKR set applies to the rule '{okrRule.ToString()}'.\n" +
                        $"Consider, that more rules may apply to an OKR set. " +
                        $"Answer by giving a single word only, 'YES' if rule applies, 'NO' " +
                        $"when rule does not apply, or 'null' if you can't decide. " +
                        $"Additionally give a short explaination for your decission. " +
                        $"No further text required!",
                    Role = "user"
                }
            },
            FunctionCall = new { name = $"{GetClassificationFunctionDefinition().Name}" },
            Functions = new List<FunctionDefinition>
            {
                GetClassificationFunctionDefinition()
            }
        };

        var resultFunction = await openAiService.Chat.CreateChatCompletionAsync(chatCompletionMessage);

        if (showConsoleOutput)
        {
            var output = string.Join("\n***********\n", chatCompletionMessage.Messages.Select(x => x.Content));
            Console.WriteLine(output);

            Console.WriteLine(
                $"******************\n" +
                $"Function: {resultFunction.Choices[0].Message?.FunctionCall?.Arguments ?? string.Empty}\n");
        }
        
        var json = resultFunction.Choices[0].Message?.FunctionCall?.Arguments ?? string.Empty;
        var parsedResult = JsonSerializer.Deserialize<FunctionArguments>(json);

        var labelEntity = new Label<OkrSet>
        {
            EntityId = okrSet.Id,
            LabelName = GetLabelName(okrRule),
            Value = parsedResult!.RuleApplies,
            Comment = parsedResult.Explanation
        };

        return labelEntity;
    }


    /// <summary>
    /// Creates a label with the count of the objectives of an OKR set.
    /// </summary>
    /// <param name="okrSet"></param>
    /// <returns></returns>
    public Label<OkrSet> CreateObjectiveCountLabel(OkrSet okrSet)
    {
        var objectiveCount = okrSet.OkrSetElements
            .Where(x => x.Type.Equals("objective"))
            .Count();

        var objectiveCountLabel = new Label<OkrSet>
        {
            EntityId = okrSet.Id,
            LabelName = "objective_count",
            Value = objectiveCount.ToString()
        };

        return objectiveCountLabel;
    }


    /// <summary>
    /// Creates a label with the count of the key results of an OKR set.
    /// </summary>
    /// <param name="okrSet"></param>
    /// <returns></returns>
    public Label<OkrSet> CreateKeyResultsCountLabel(OkrSet okrSet)
    {
        var keyResultsCount = okrSet.OkrSetElements
            .Where(x => x.Type.Equals("keyresult"))
            .Count();

        var keyResultsCountLabel = new Label<OkrSet>
        {
            EntityId = okrSet.Id,
            LabelName = "keyresult_count",
            Value = keyResultsCount.ToString()
        };

        return keyResultsCountLabel;
    }

}
