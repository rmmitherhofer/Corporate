using Candidatos.Data;
using Microsoft.EntityFrameworkCore;
using NedMonitor.EF.Configurations;
using NedMonitor.EF.Interceptors;

namespace Candidatos.Api.Configurations;

public static class DbContextConfiguration
{
    private const string CONNECTION_CANDIDATE_KEY = "CNN_DB_CANDIDATO";

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(IServiceCollection));
        ArgumentNullException.ThrowIfNull(configuration, nameof(IConfiguration));

        services.AddNedMonitorEfInterceptors();

        services.AddDbContext<CandidatoContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString(CONNECTION_CANDIDATE_KEY));
            options.EnableSensitiveDataLogging(false);
            options.UseLoggerFactory(LoggerFactory.Create(_ => { }));
            options.AddInterceptors(sp.GetRequiredService<EfQueryCounter>());
        });

        services.AddScoped<CandidatoContext>();

        return services;
    }
}
