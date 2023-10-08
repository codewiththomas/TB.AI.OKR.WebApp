using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.Emit;
using System.Text.Json;
using TB.AI.OKR.WebApp;
using TB.AI.OKR.WebApp.Extensions;
using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.OpenAI.ApiClient;
using TB.OpenAI.ApiClient.Abstract.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Chat;

namespace TB.Tools.DataLabeler;

internal class Labeler
{
    private readonly IConfiguration _configuration;

    public Labeler(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    /// <summary>
    /// Labels an OKR-set.
    /// </summary>
    /// <param name="okrSet"></param>
    /// <param name="okrRule"></param>
    /// <param name="showConsoleOutput"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<Label<OkrSet>> LabelOkrSet(OkrSet okrSet, OkrRule okrRule, bool showConsoleOutput = true)
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
            Value = parsedResult.RuleApplies,
            Comment = parsedResult.Explanation
        };

        return labelEntity;
    }


    /// <summary>
    /// Labels an OkrSetElement of the types "keyresult" and "objective".
    /// </summary>
    /// <param name="okrSetElement"></param>
    /// <param name="okrRule"></param>
    /// <param name="showConsoleOutput"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<Label<OkrSetElement>> LabelOkrSetElement(OkrSetElement okrSetElement, OkrRule okrRule, bool showConsoleOutput = true)
    {
        var expectedScope = okrSetElement.Type switch
        {
            "objective" => OkrRuleScopes.Objective,
            "keyresult" => OkrRuleScopes.KeyResult,
            _ => throw new ArgumentException("Unexpected Scope!")
        };

        if (!okrRule.Scope.Equals(expectedScope))
        {
            throw new ArgumentException(
                $"Type of OkrSetElement '{okrSetElement.Type}' " +
                $"does not match the rule scope '{okrRule.Scope}'!");
        }

        var entityType = okrRule.Scope.GetDescription();

        var openAiService = new OpenAiApiClient(_configuration);

        var chatCompletionMessage = new CreateChatCompletionRequest
        {
            Model = "gpt-3.5-turbo",
            Messages = new List<CreateChatCompletionRequestMessage>
            {
                new CreateChatCompletionRequestMessage
                {
                    Content =
                        $"You are a professional data labeler. You have to task to label a dataset of " +
                        $"OKR sets (Objectives and Key Results) towards certain rules. In this case you have to " +
                        $"label the {entityType} part only, you do not care about the rest of the set.",
                    Role = "system"
                },
                new CreateChatCompletionRequestMessage
                {
                    Content =
                        $"This is the {entityType} you have to label:\n" +
                        $"**BEGIN** \n{okrSetElement.Text}**END**\n" +
                        $"Decide, if the given {entityType} applies to the rule '{okrRule}'.\n" +
                        $"Consider, that more rules may apply to a {entityType}. " +
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

        var labelEntity = new Label<OkrSetElement>
        {
            EntityId = okrSetElement.Id,
            LabelName = GetLabelName(okrRule),
            Value = parsedResult.RuleApplies,
            Comment = parsedResult.Explanation
        };

        return labelEntity;
    }


    private string GetLabelName(OkrRule okrRule)
        => $"rule_{okrRule.Id}";


    private FunctionDefinition GetClassificationFunctionDefinition()
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
