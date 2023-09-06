using Clientes.Data;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Api.Configurations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ClienteContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CNN_DB_CLIENTE")));

        services.AddScoped<ClienteContext>();

        return services;
    }
}
