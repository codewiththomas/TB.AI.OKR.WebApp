using Microsoft.Extensions.Configuration;
using TB.OpenAI.ApiClient.Contracts.Chat;
using TB.OpenAI.ApiClient.Contracts.Models;
using TB.OpenAI.ApiClient.Repositories.Chat;
using TB.OpenAI.ApiClient.Repositories.Models;

namespace TB.OpenAI.ApiClient;

public class OpenAiApiClient : IOpenAiApiClient
{
    #region Public Properties (=Endpoints)

    public IModelsRepository Models { get; set; }
    public IChatRepository Chat { get; set; }

    #endregion


    public OpenAiApiClient(string? apiKey = null, string? organizationId = null, string? baseUrl = null)
    {
        if (apiKey == null)
        {
            throw new ArgumentNullException(nameof(apiKey));
        }

        OpenAiApiSettings.ApiKey = apiKey;
        OpenAiApiSettings.OrganizationId = organizationId;

        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            OpenAiApiSettings.BaseUrl = "https://api.openai.com";
        }

        Chat = new ChatRepository();
        Models = new ModelsRepository();        
    }

    public OpenAiApiClient(IConfiguration configuration)
        : this(configuration["OpenAiSettings:ApiKey"], configuration["OpenAiSettings:OrgId"], configuration["OpenAiSettings:BaseUrl"])
    { }
}