using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Notifications.Interfaces;
using Notifications.Messages;
using Responses;

namespace Api.Service.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class MainController : ControllerBase
{
    protected readonly INotificationHandler _notification;

    protected MainController(INotificationHandler notification)
    {
        _notification = notification;
    }
    protected ActionResult CustomResponse(object? result = null)
    {
        if (result is not null)
        {
            switch (result.GetType().Name)
            {
                case nameof(NotFoundResponse):
                    return CustomResponse(result as NotFoundResponse);
                case nameof(ModelStateDictionary):
                    return CustomResponse(result as ModelStateDictionary);
                case nameof(ValidationResult):
                    return CustomResponse(result as ValidationResult);
            }
        }
        if (OperacaoEhValida())
            return Ok(result);

        ValidationResponse response = new(_notification.Get());

        if (response.Validations.Select(v => v.Level).Contains(LogLevel.Error))
            return Problem(detail: JsonConvert.SerializeObject(response));

        return BadRequest(response);
    }

    private ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        foreach (var erro in modelState.Values.SelectMany(e => e.Errors))
        {
            Notificar(nameof(ModelStateDictionary), null, erro.ErrorMessage);
        }
        return CustomResponse();
    }

    private ActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var erro in validationResult.Errors)
        {
            Notificar(nameof(ValidationResult), erro.PropertyName, erro.ErrorMessage);
        }
        return CustomResponse();
    }

    private ActionResult CustomResponse(NotFoundResponse response)
        => NotFound(response);
    protected ActionResult NotFound(string? detail = null)
        => NotFound(new NotFoundResponse(detail));
    protected bool OperacaoEhValida()
        => !_notification.HasNotifications();
    protected void Notificar(string type, string key, string mensagem)
        => _notification.Notify(new Notification(type, key, mensagem));
    protected void Notificar(string key, string mensagem)
        => _notification.Notify(new Notification(key, mensagem));
    protected void Notificar(Exception exception)
        => _notification.Notify(new Notification(LogLevel.Error, exception.GetType().Name, exception.GetType().Name, exception.Message, exception?.StackTrace));
    protected void LimparNotificacoes()
        => _notification.Clear();
}