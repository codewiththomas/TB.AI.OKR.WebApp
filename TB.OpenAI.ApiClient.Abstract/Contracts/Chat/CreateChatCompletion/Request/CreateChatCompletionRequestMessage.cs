using System.Text.Json.Serialization;

namespace TB.OpenAI.ApiClient.Contracts.Chat;

public class CreateChatCompletionRequestMessage
{
    /// <summary>
    /// The role of the messages author. One of system, user, assistant, or function.
    /// </summary>
    [JsonPropertyName("role")]
    [JsonRequired]
    public string Role { get; set; } = "user";

    /// <summary>
    /// The contents of the message. content is required for all messages, and may be 
    /// null for assistant messages with function calls.
    /// </summary>
    [JsonPropertyName("content")]
    [JsonRequired]
    public string Content { get; set; } = string.Empty;

}
