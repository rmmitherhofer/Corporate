using Api.Service.Controllers;
using Candidatos.Business.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;

namespace Candidatos.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/historico")]
public class HistoricoController : MainController
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoRepository _candidatoRepository;

    public HistoricoController(INotificationHandler notification, ILogger<CandidatoController> logger, ICandidatoRepository candidatoRepository) : base(notification)
    {
        _logger = logger;
        _candidatoRepository = candidatoRepository;
    }
}
