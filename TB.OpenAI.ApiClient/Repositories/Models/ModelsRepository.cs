using TB.OpenAI.ApiClient.Contracts.Models;

namespace TB.OpenAI.ApiClient.Repositories.Models;

public class ModelsRepository : BaseRepository, IModelsRepository
{
    public async Task<string> ListModelsAsync()
    {
        var client = GetAuthenticatedHttpClient();
        var response = await client.GetAsync("/v1/models");
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}
