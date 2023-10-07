using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TB.OpenAI.ApiClient.Contracts.Chat;

public class FunctionCall
{
    [JsonPropertyName("arguments")]
    public string Arguments { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

}
