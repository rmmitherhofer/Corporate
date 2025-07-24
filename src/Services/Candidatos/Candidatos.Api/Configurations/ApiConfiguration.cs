using Zypher.Api.Foundation.Configurations;

namespace Candidatos.Api.Configurations;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(IServiceCollection));
        ArgumentNullException.ThrowIfNull(configuration, nameof(IConfiguration));

        services.AddZypherApiFoundation(configuration, environment);

        services.AddRepositories();
        services.AddFactories();
        services.AddAutoMapper();
        services.AddDbContext(configuration);

        return services;
    }

    public static WebApplication UseApiConfig(this WebApplication app)
    {
        app.UseZypherApiFoundation();

        return app;
    }
}
