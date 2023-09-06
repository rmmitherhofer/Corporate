using Candidatos.Api.Configurations.AutoMapper;
using Candidatos.Business.Factories;
using Candidatos.Business.Interfaces.Repositories;
using Candidatos.Data.Repositories;

namespace Candidatos.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<ICandidatoRepository, CandidatoRepository>();

        return services;
    }

    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services
            .AddScoped<IParecerPdfFactory, ParecerPdfFactory>();

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(FilterRequestToFilterProfile));

        return services;
    }
}
