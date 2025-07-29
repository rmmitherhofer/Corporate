using NedMonitor.Http.Configurations;
using NedMonitor.Http.Handlers;
using PanPan.WebApp.HttpServices;

namespace PanPan.WebApp.Configurations;

public static class HttpServiceConfiguration
{
    public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(IServiceCollection));
        ArgumentNullException.ThrowIfNull(configuration, nameof(IConfiguration));

        services.AddNedMonitorHttp();

        services.AddHttpClient<ICandidatoHttpService, CandidatoHttpService>(services =>
            services.BaseAddress = new Uri(configuration.GetSection("Apis:Candidato").Value)
        )
        .AddHttpMessageHandler<NedMonitorHttpLoggingHandler>();

        services.AddHttpClient<IClienteHttpService, ClienteHttpService>(services =>
            services.BaseAddress = new Uri(configuration.GetSection("Apis:Cliente").Value)
        )
        .AddHttpMessageHandler<NedMonitorHttpLoggingHandler>();

        services.AddHttpClient<IVagaHttpService, VagaHttpService>(services =>
            services.BaseAddress = new Uri(configuration.GetSection("Apis:Vaga").Value)
        )
        .AddHttpMessageHandler<NedMonitorHttpLoggingHandler>();


        services.AddHttpClient<ITalentMindHttpService, TalentMindHttpService>(services =>
        {
            services.BaseAddress = new Uri(configuration.GetSection("Apis:TalentMind").Value);
            services.Timeout = TimeSpan.FromMinutes(30);
        })
        .AddHttpMessageHandler<NedMonitorHttpLoggingHandler>();

        return services;
    }
}