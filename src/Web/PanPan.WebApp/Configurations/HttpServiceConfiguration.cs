using Microsoft.Extensions.DependencyInjection;
using PanPan.WebApp.HttpServices;

namespace PanPan.WebApp.Configurations;

public static class HttpServiceConfiguration
{
    public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ICandidatoHttpService, CandidatoHttpService>(services =>
            services.BaseAddress = new Uri(configuration.GetSection("Apis:Candidato").Value)
        );

        services.AddHttpClient<IClienteHttpService, ClienteHttpService>(services =>
            services.BaseAddress = new Uri("https://localhost:7032")
        );

        services.AddHttpClient<IVagaHttpService, VagaHttpService>(services =>
            services.BaseAddress = new Uri("https://localhost:7105")
        );

        return services;
    }
}