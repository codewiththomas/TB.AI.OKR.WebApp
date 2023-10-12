using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TB.AI.OKR.WebApp;
using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.OpenAI.ApiClient.Abstract.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Chat;
using TB.OpenAI.ApiClient;
using Microsoft.Fast.Components.FluentUI;
using TB.Tools.Readability;

namespace TB.Tools.DataLabeler.Services.Abstract;

public abstract class OkrSetElementLabelService : LabelService<OkrSetElement>
{
    /// <summary>
    /// Constructor for dependency injection
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="okrSetElementType"></param>
    /// <exception cref="ArgumentException"></exception>
    public OkrSetElementLabelService(IConfiguration configuration, string okrSetElementType) : base(configuration)
    {
        OkrSetElementType = okrSetElementType;

        var validElementType = okrSetElementType switch
        {
            "objective" => true,
            "keyresult" => true,
            _ => false
        };

        if (!validElementType)
        {
            throw new ArgumentException($"Invalid element type {OkrSetElementType}!");
        }
    }

    public string OkrSetElementType { get; }

    public int? ElementNumber { get; set; }


    /// <summary>
    /// Labels an OkrSetElement of the type "objective".
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="okrRule"></param>
    /// <param name="showConsoleOutput"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task<Label<OkrSetElement>> CreateLabelByRule(OkrSetElement okrSetElement, OkrRule okrRule, bool showConsoleOutput = true)
    {
        var labelProcessStartDateTime = DateTime.Now;

        var expectedOkrElementType = OkrSetElementType;

        if (!okrSetElement.Type.Equals(OkrSetElementType))
        {
            throw new ArgumentException($"Wrong element type! Expeced {OkrSetElementType} but it was {okrSetElement.Type}");
        }

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

        var labelProcessEndDateTime = DateTime.Now;

        var labelEntity = new Label<OkrSetElement>
        {
            EntityId = okrSetElement.Id,
            LabelName = GetLabelName(okrRule) + (ElementNumber == null ? string.Empty : $"_{ElementNumber}"),
            Value = parsedResult!.RuleApplies,
            Comment = parsedResult.Explanation,
            LabelingDuration = labelProcessEndDateTime - labelProcessStartDateTime
        };

        return labelEntity;
    }



    public Label<OkrSetElement> CreateReadabilityLabel(OkrSetElement okrSetElement, ReadabilityAlgorithms algorithm = ReadabilityAlgorithms.AutomatedReadabilityIndex)
    {
        var labelProcessStartDateTime = DateTime.Now;

        if (!okrSetElement.Type.Equals(OkrSetElementType))
        {
            throw new ArgumentException($"Wrong element type! Expeced {OkrSetElementType} but it was {okrSetElement.Type}");
        }

        var readabilityScore = okrSetElement.Text.CalculateReadability(algorithm);

        var labelProcessEndDateTime = DateTime.Now;

        var labelEntity = new Label<OkrSetElement>
        {
            EntityId = okrSetElement.Id,
            LabelName = "readability_" + algorithm.ToString().ToLower() + (ElementNumber == null ? string.Empty : $"_{ElementNumber}"),
            Value = readabilityScore.ToString(),
            LabelingDuration = labelProcessEndDateTime - labelProcessStartDateTime
        };

        return labelEntity;
    }
}
