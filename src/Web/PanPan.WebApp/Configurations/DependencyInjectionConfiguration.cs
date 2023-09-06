using PanPan.WebApp.Configurations.AutoMapper;

namespace PanPan.WebApp.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection Resolve(this IServiceCollection services)
    {

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingViewModelToRequestProfile), typeof(MappingResponseToViewModelProfile));

        return services;
    }


}

