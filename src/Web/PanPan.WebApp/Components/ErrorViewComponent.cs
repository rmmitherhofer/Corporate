using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;

namespace PanPan.WebApp.Components;

public class ErrorViewComponent : ViewComponent
{
    private readonly INotificationHandler _notification;

    public ErrorViewComponent(INotificationHandler notification)
    {
        _notification = notification;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var notifications = await Task.FromResult(_notification.Get());

        return View(notifications.Where(n => n.Level >= LogLevel.Error).ToList());
    }
}
