using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TB.OpenAI.ApiClient.Contracts.Chat;

public class CreateChatCompletionResponseMessageDto
{
    /// <summary>
    /// The role of the author of this message.
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// The contents of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

}
