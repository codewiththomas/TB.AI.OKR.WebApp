using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TB.OpenAI.ApiClient.Abstract.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Chat;
using TB.OpenAI.ApiClient;
using Microsoft.Fast.Components.FluentUI;
using TB.Tools.Readability;
using TB.AI.OKR.Core.Domain;
using OkrML;
using TB.GPT4All.ApiClient;

namespace TB.Tools.DataLabeler.Services.Abstract;

public abstract class OkrSetElementLabelService : LabelService<OkrSetElement>
{
    /// <summary>
    /// Constructor for dependency injection
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="okrSetElementType"></param>
    /// <exception cref="ArgumentException"></exception>
    public OkrSetElementLabelService(IConfiguration configuration, LabelProviders labelProvider, string okrSetElementType) 
        : base(configuration, labelProvider)
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
        string ruleApplies = "NO";
        string explanation = string.Empty;

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



        if (LabelProvider == LabelProviders.Annotator
            || LabelProvider == LabelProviders.OpenAI_GPT_35_Turbo
            || LabelProvider == LabelProviders.OpenAI_GPT_35_Turbo
            || LabelProvider == LabelProviders.GPT4All_Falcon
            || LabelProvider == LabelProviders.GPT4All_Hermes_LLaMA2)
        {
            IOpenAiApiClient aiService;

            if (LabelProvider == LabelProviders.GPT4All_Falcon
                || LabelProvider == LabelProviders.GPT4All_Hermes_LLaMA2)
            {
                aiService = new GPT4AllApiClient(_configuration);
            }
            else
            {
                aiService = new OpenAiApiClient(_configuration);
            }

            var chatCompletionMessage = new CreateChatCompletionRequest
            {
                Model = LabelProvider.GetOpenAiModelName(),
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

            try
            {
                var resultFunction = await aiService.Chat.CreateChatCompletionAsync(chatCompletionMessage);

                if (showConsoleOutput)
                {
                    var output = string.Join("\n***********\n", chatCompletionMessage.Messages.Select(x => x.Content));
                    Console.WriteLine(output);

                    Console.WriteLine(
                        $"******************\n" +
                        $"Function: {resultFunction.Choices[0].Message?.FunctionCall?.Arguments ?? string.Empty}\n");
                }

                var json = resultFunction.Choices[0].Message?.FunctionCall?.Arguments ?? string.Empty;
                
                if (!string.IsNullOrEmpty(json))
                {
                    var parsedResult = JsonSerializer.Deserialize<FunctionArguments>(json);

                    ruleApplies = parsedResult!.RuleApplies;
                    explanation = parsedResult!.Explanation ?? string.Empty;
                }
                else //GPT4All does not support functions YET. In this case the classic message has to be read
                {
                    var answer = resultFunction.Choices[0].Message?.Content;

                    if (!string.IsNullOrWhiteSpace(answer)
                        && answer.Contains("YES", StringComparison.OrdinalIgnoreCase))
                    {
                        ruleApplies = "YES";
                        explanation = answer;
                    }
                }

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("ERROR: " + ex.Message);
            }

        }

        else if (LabelProvider == LabelProviders.ML)
        {
            //Load sample data
            var sampleData = new OkrSetElementLabeler.ModelInput()
            {
                Text = okrSetElement.Text,
                Type = OkrSetElementType,
                LabelName = GetLabelName(okrRule),
            };

            //Load model and predict output
            var result = OkrSetElementLabeler.Predict(sampleData);
            ruleApplies = result.PredictedLabel ? "YES" : "NO";
            explanation = $"Score = {result.Score}, Probability = {result.Probability}.";
        }

        else
        {
            throw new NotImplementedException();
        }



        var labelProcessEndDateTime = DateTime.Now;

        var labelEntity = new Label<OkrSetElement>
        {
            EntityId = okrSetElement.Id,
            LabelName = GetLabelName(okrRule) + (ElementNumber == null ? string.Empty : $"_{ElementNumber}"),
            LabelProvider = LabelProvider.GetDescription() ?? LabelProvider.ToString(),
            Value = ruleApplies,
            Comment = explanation,
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
            LabelProvider = LabelProvider.GetDescription() ?? LabelProvider.ToString(),
            Value = readabilityScore.ToString(),
            LabelingDuration = labelProcessEndDateTime - labelProcessStartDateTime
        };

        return labelEntity;
    }
}
