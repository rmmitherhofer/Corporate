using Candidatos.Data;
using Microsoft.EntityFrameworkCore;

namespace Candidatos.Api.Configurations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CandidatoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CNN_DB_CANDIDATO")));

        services.AddScoped<CandidatoContext>();

        return services;
    }
}
