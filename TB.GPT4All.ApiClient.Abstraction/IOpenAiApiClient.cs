using TB.OpenAI.ApiClient;
using TB.OpenAI.ApiClient.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Models;

namespace TB.GPT4All.ApiClient;

public interface IGPT4AllApiClient : IOpenAiApiClient
{
    //public IChatRepository Chat { get; set; }
    //public IModelsRepository Models { get; set; }
}
