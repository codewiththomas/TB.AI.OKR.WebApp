using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TB.OpenAI.ApiClient.Abstract.Contracts.Chat;

public class CreateChatCompletionRequestFunctionParameters
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "object";

    public string Properties { get; set; } = string.Empty;
}
