using NedMonitor.Configurations;
using PanPan.WebApp.Middlewares;
using Zypher.Extensions.Core;
using Zypher.Logs.Configurations;
using Zypher.Notifications.Configurations;

namespace PanPan.WebApp.Configurations;

public static class WebappConfiguration
{
    public static IServiceCollection AddWebAppConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(IServiceCollection));
        ArgumentNullException.ThrowIfNull(configuration, nameof(IConfiguration));

        services.AddHttpContextAccessor();

        configuration.Set(environment);

        services.AddControllersWithViews(); 
        
        services.AddNotificationConfig();

        services.AddNedMonitor(configuration);

        services.AddHttpServices(configuration);
        services.AddAutoMapper();
        return services;
    }

    public static IApplicationBuilder UseWebAppConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(IApplicationBuilder));

        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseNedMonitor();

        app.UseNotificationConfig();

        app.UseLogDecoratorConfig();

        app.TryUseMiddleware<ExceptionMiddleware>();

        app.UseNedMonitorMiddleware();

        app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"));


        return app;
    }
}
