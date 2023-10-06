using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using TB.OpenAI.ApiClient.Abstract.Contracts.Chat;

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
    /// Controls how the model calls functions. "none" means the model will not call a function and 
    /// instead generates a message. "auto" means the model can pick between generating a message 
    /// or calling a function. Specifying a particular function via {"name": "my_function"} forces 
    /// the model to call that function. "none" is the default when no functions are present. 
    /// "auto" is the default if functions are present.
    /// </summary>
    [JsonPropertyName("function_call")]
    public string FunctionCall { get; set; } = "none";

    /// <summary>
    /// A list of functions the model may generate JSON inputs for.
    /// </summary>
    [JsonPropertyName("functions")]
    public IList<CreateChatCompletionRequestFunction> Functions { get; set; }
        = new List<CreateChatCompletionRequestFunction>();

    /// <summary>
    /// The maximum number of tokens to generate in the completion. The token count of your prompt 
    /// plus max_tokens cannot exceed the model's context length. Optional. Defaults to 16.
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 1024;
}
