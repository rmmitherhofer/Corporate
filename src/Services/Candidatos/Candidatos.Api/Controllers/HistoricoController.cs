using Api.Service.Controllers;
using Candidatos.Business.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Candidatos.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/historico")]
public class HistoricoController : MainController
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoRepository _candidatoRepository;

    public HistoricoController(ILogger<CandidatoController> logger, ICandidatoRepository candidatoRepository)
    {
        _logger = logger;
        _candidatoRepository = candidatoRepository;
    }
}
