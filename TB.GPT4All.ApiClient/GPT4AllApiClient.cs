using Microsoft.Extensions.Configuration;
using TB.OpenAI.ApiClient;

namespace TB.GPT4All.ApiClient;

public class GPT4AllApiClient : OpenAiApiClient, IGPT4AllApiClient
{
    public GPT4AllApiClient(string? baseUrl = null) 
        : base(string.Empty, null, baseUrl ?? "http://localhost/4891")
    { }


    public GPT4AllApiClient(IConfiguration configuration)
        : this(configuration["GPT4AllSettings:BaseUrl"]) 
    { }
}
