using Microsoft.AspNetCore.Mvc;
using Zypher.Notifications.Interfaces;

namespace PanPan.WebApp.Components;

public class SuccessViewComponent : ViewComponent
{
    private readonly INotificationHandler _notification;

    public SuccessViewComponent(INotificationHandler notification)
    {
        _notification = notification;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}