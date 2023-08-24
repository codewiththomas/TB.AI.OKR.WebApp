using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TB.OpenAI.ApiClient.Contracts.Chat;

public class CreateChatCompletionResponse
{
    /// <summary>
    /// A unique identifier for the chat completion.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The object type, which is always chat.completion.
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// A unix timestamp of when the chat completion was created.
    /// </summary>
    [JsonPropertyName("created")]
    public Int64 Created { get; set; }

    /// <summary>
    /// The model used for the chat completion.
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// A list of chat completion choices. Can be more than one if n is greater than 1.
    /// </summary>
    [JsonPropertyName("choices")]
    public IList<CreateChatCompletionResponseChoiceDto> Choices { get; set; }
        = new List<CreateChatCompletionResponseChoiceDto>();

    /// <summary>
    /// Usage statistics for the completion request.
    /// </summary>
    [JsonPropertyName("usage")]
    public CreateChatCompletionResponseUsageDto Usage { get; set; } = new();

    [NotMapped]
    public List<string> DebugInfos { get; set; } = new();
}
