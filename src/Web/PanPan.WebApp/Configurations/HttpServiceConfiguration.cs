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
            services.BaseAddress = new Uri(configuration.GetSection("Apis:Cliente").Value)
        );

        services.AddHttpClient<IVagaHttpService, VagaHttpService>(services =>
            services.BaseAddress = new Uri(configuration.GetSection("Apis:Vaga").Value)
        );

        return services;
    }
}