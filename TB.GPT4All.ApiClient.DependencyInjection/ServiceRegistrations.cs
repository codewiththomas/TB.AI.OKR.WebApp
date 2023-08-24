using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TB.GPT4All.ApiClient;

public static class ServiceRegistration
{
    /// <summary>
    /// Registers an OpenAI API client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddGPT4AllApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IGPT4AllApiClient, GPT4AllApiClient>(options => 
            new GPT4AllApiClient(configuration)
        );
        return services;
    }

    /// <summary>
    /// Registers an OpenAI API client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddGPT4AllApiClient(this IServiceCollection services)
    {
        services.AddTransient<IGPT4AllApiClient, GPT4AllApiClient>();
        return services;
    }

}