using Notifications.Messages;
using System.Net;

namespace Responses;

public class ErrorResponse
{
    public HttpStatusCode? StatusCode { get; private set; }
    /// <summary>
    /// Notificações
    /// </summary>
    public ICollection<Notification> Errors { get; set; }

    public ErrorResponse(HttpStatusCode? statusCode, ICollection<Notification> notifications)
    {
        StatusCode = statusCode;
        Errors = notifications;
    }
}
