using Microsoft.EntityFrameworkCore;
using Vagas.Data;

namespace Vagas.Api.Configurations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VagasContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CNN_DB_VAGA")));

        services.AddScoped<VagasContext>();

        return services;
    }
}
