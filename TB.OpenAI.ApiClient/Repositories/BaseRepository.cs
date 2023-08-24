using System.Net.Http.Headers;

namespace TB.OpenAI.ApiClient.Repositories;

public abstract class BaseRepository
{
    protected HttpClient GetAuthenticatedHttpClient()
    {
        var httpClient = new HttpClient();
        
        httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", OpenAiApiSettings.ApiKey);
       
        if (OpenAiApiSettings.OrganizationId != null) 
        {
            httpClient.DefaultRequestHeaders.Add(
                "OpenAI-Organization", 
                OpenAiApiSettings.OrganizationId);
        }

        httpClient.BaseAddress = new Uri(OpenAiApiSettings.BaseUrl);

        return httpClient;
    }
}
