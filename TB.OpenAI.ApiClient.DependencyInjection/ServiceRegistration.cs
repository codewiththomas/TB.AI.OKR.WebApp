using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TB.OpenAI.ApiClient;

public static class ServiceRegistration
{
    /// <summary>
    /// Registers an OpenAI API client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenAiApiClient(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddTransient<IOpenAiApiClient, OpenAiApiClient>(options => 
            new OpenAiApiClient(configuration)
        );

        return services;
    }
}