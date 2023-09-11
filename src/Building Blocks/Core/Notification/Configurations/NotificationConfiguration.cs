using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Notifications.Handler;
using Notifications.Interfaces;

namespace Notifications.Configurations;

public static class NotificationConfiguration
{
    /// <summary>
    /// Configura e resolve o contrado do INotificationHandler
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddNotificationConfig(this IServiceCollection services)
    {
        services.TryAddScoped<INotificationHandler, NotificationHandler>();

        return services;
    }
}
