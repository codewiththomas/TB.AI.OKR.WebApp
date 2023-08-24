using TB.OpenAI.ApiClient.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Models;

namespace TB.OpenAI.ApiClient;

public interface IOpenAiApiClient
{
    public IChatRepository Chat { get; set; }
    public IModelsRepository Models { get; set; }
}
