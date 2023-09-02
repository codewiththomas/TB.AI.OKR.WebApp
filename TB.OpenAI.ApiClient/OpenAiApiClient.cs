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

    private OpenAiApiSettings _settings;

    /// <summary>
    /// Creates a new instance of an OpenAiApiClient
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="organizationId"></param>
    /// <param name="baseUrl"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public OpenAiApiClient(string? apiKey = null, string? organizationId = null, string? baseUrl = null)
    {
        if (apiKey == null)
        {
            throw new ArgumentNullException(nameof(apiKey));
        }

        _settings = new OpenAiApiSettings
        {
            ApiKey = apiKey,
            OrganizationId = organizationId
        };

        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            _settings.BaseUrl = "https://api.openai.com";
        }
        else
        {
            _settings.BaseUrl = baseUrl;
        }

        Chat = new ChatRepository(_settings);
        Models = new ModelsRepository(_settings);        
    }


    /// <summary>
    /// Creates a new instance of an OpenAiApiClient by using IConfiguration.
    /// </summary>
    /// <param name="configuration"></param>
    public OpenAiApiClient(IConfiguration configuration)
        : this(configuration["OpenAiSettings:ApiKey"], configuration["OpenAiSettings:OrgId"], configuration["OpenAiSettings:BaseUrl"])
    { }
}