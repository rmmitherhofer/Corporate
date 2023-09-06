using Vagas.Business.Interfaces.Repositories;
using Vagas.Data.Repositories;

namespace Vagas.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IVagaRepository, VagaRepository>();

        return services;
    }
}
