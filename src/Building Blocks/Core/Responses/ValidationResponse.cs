using Microsoft.AspNetCore.Mvc;
using Notifications.Messages;

namespace Responses;

/// <summary>
/// Classe de resposta de validações
/// </summary>
public class ValidationResponse : ProblemDetails
{
    /// <summary>
    /// Notificações
    /// </summary>
    public ICollection<Notification> Validations { get; set; }

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="messages"></param>
    public ValidationResponse(ICollection<Notification> notifications)
    {
        Validations = notifications;
    }
}