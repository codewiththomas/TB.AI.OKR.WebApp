using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TB.OpenAI.ApiClient.Abstract.Contracts.Chat;

public class CreateChatCompletionRequestFunction
{
    /// <summary>
    /// A description of what the function does, used by the model to choose when and how to call the function.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The name of the function to be called. Must be a-z, A-Z, 0-9, or contain underscores and dashes, 
    /// with a maximum length of 64.
    /// </summary>
    [JsonRequired]
    [MaxLength(64)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The parameters the functions accepts, described as a JSON Schema object. 
    /// See the guide for examples, and the JSON Schema reference for documentation about the format.
    /// </summary>
    [JsonPropertyName("parameters")]
    [JsonRequired]
    public string Parameters { get; set; } = string.Empty;


}
