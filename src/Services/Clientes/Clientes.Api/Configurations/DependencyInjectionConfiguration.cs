using Clientes.Api.Configurations.AutoMapper;
using Clientes.Business.Interfaces.Repositories;
using Clientes.Data.Repositories;

namespace Clientes.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IClienteRepository, ClienteRepository>();

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(FilterRequestToFilterProfile));

        return services;
    }
}
