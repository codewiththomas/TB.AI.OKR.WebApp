using System.Text.Json.Serialization;

namespace TB.OpenAI.ApiClient.Contracts.Chat;

public class CreateChatCompletionRequest
{
    /// <summary>
    /// ID of the model to use. You can use the List models API to see all of your available models, 
    /// or see our Model overview (<see href="https://platform.openai.com/docs/models/overview"/>)
    /// for descriptions of them.
    /// </summary>
    [JsonPropertyName("model")]
    [JsonRequired]
    public string Model { get; set; } = "gpt-3.5-turbo";

    /// <summary>
    /// A list of messages comprising the conversation so far.
    /// </summary>
    [JsonPropertyName("messages")]
    public IList<CreateChatCompletionRequestMessage> Messages { get; set; } 
        = new List<CreateChatCompletionRequestMessage>();

    /// <summary>
    /// The maximum number of tokens to generate in the completion. The token count of your prompt 
    /// plus max_tokens cannot exceed the model's context length. Optional. Defaults to 16.
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 1024;
}
