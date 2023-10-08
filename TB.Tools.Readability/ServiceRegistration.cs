using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TB.Tools.Readability;

public static class ServiceRegistration
{
    /// <summary>
    /// Registers the readability service to the dependency injection container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddReadabilityService(this IServiceCollection services)
    {
        services.AddTransient<IReadabilityService, ReadabilityService>(options =>
            new ReadabilityService()
        );
        return services;
    }
}
