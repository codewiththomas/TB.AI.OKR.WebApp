using System.Net.Http.Headers;

namespace TB.OpenAI.ApiClient.Repositories;

public abstract class BaseRepository
{
    private readonly OpenAiApiSettings _settings;

    public BaseRepository(OpenAiApiSettings settings)
    {
        _settings = settings;
    }

    protected HttpClient GetAuthenticatedHttpClient()
    {
        var httpClient = new HttpClient();
        
        if (!string.IsNullOrEmpty(_settings.ApiKey))
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
        }
       
        if (_settings.OrganizationId != null) 
        {
            httpClient.DefaultRequestHeaders.Add(
                "OpenAI-Organization",
                _settings.OrganizationId);
        }

        httpClient.BaseAddress = new Uri(_settings.BaseUrl);

        return httpClient;
    }
}
