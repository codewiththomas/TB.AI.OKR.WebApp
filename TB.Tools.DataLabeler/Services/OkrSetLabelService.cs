using Microsoft.Extensions.Configuration;
using OkrML;
using System.Data;
using System.Text.Json;
using TB.AI.OKR.Core.Application;
using TB.AI.OKR.Core.Domain;
using TB.GPT4All.ApiClient;
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
    public OkrSetLabelService(IConfiguration configuration, LabelProviders labelProvider) 
        : base(configuration, labelProvider)
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
        var labelProcessStartDateTime = DateTime.Now;
        string ruleApplies = "NO";
        string explanation = string.Empty;

        if (okrRule.Scope != OkrRuleScopes.OkrSet)
        {
            throw new ArgumentException("Only rules with scope OKR set may be applied to OKR sets!");
        }

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
                Text = okrSet.ToSetString(),
                Type = @"okrset",
                LabelName = GetLabelName(okrRule),
            };

            //Load model and predict output
            var result = OkrSetElementLabeler.Predict(sampleData);

        }

        else
        {
            throw new NotImplementedException();
        }


        var labelProcessEndDateTime = DateTime.Now;

        var labelEntity = new Label<OkrSet>
        {
            EntityId = okrSet.Id,
            LabelName = GetLabelName(okrRule),
            LabelProvider = LabelProvider.GetDescription() ?? LabelProvider.ToString(),
            Value = ruleApplies, //parsedResult!.RuleApplies,
            Comment = explanation, //parsedResult.Explanation,
            LabelingDuration = labelProcessEndDateTime - labelProcessStartDateTime
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
        var labelProcessStartDateTime = DateTime.Now;

        var objectiveCount = okrSet.OkrSetElements
            .Where(x => x.Type.Equals("objective"))
            .Count();

        var labelProcessEndDateTime = DateTime.Now;

        var objectiveCountLabel = new Label<OkrSet>
        {
            LabelProvider = LabelProvider.GetDescription() ?? LabelProvider.ToString(),
            EntityId = okrSet.Id,
            LabelName = "objective_count",
            Value = objectiveCount.ToString(),
            LabelingDuration = labelProcessEndDateTime - labelProcessStartDateTime
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
        var labelProcessStartDateTime = DateTime.Now;

        var keyResultsCount = okrSet.OkrSetElements
            .Where(x => x.Type.Equals("keyresult"))
            .Count();

        var labelProcessEndDateTime = DateTime.Now;

        var keyResultsCountLabel = new Label<OkrSet>
        {
            EntityId = okrSet.Id,
            LabelName = "keyresult_count",
            LabelProvider = LabelProvider.GetDescription() ?? LabelProvider.ToString(),
            Value = keyResultsCount.ToString(),
            LabelingDuration = labelProcessEndDateTime - labelProcessStartDateTime
        };

        return keyResultsCountLabel;
    }

}
