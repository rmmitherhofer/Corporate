using Microsoft.EntityFrameworkCore;
using Recrutamentos.Data;

namespace Recrutamentos.Api.Configurations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RecrutamentoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CNN_DB_RECRUITMENT")));

        services.AddScoped<RecrutamentoContext>();

        return services;
    }
}
