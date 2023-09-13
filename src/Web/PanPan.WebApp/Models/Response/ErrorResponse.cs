using System.Net;

namespace PanPan.WebApp.Models.Response;

public class ErrorResponse
{
    public HttpStatusCode? StatusCode { get; set; }
    public IEnumerable<NotificationResponse> Errors { get; set; } 
}
