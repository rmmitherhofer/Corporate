using Candidatos.Business.Factories;
using Candidatos.Business.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Zypher.Api.Foundation.Controllers;
using Zypher.Notifications.Interfaces;

namespace Candidatos.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/parecer")]
public class ParecerController : MainController
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IParecerPdfFactory _parecerPdfFactory;

    public ParecerController(INotificationHandler notification,  ILogger<CandidatoController> logger, ICandidatoRepository candidatoRepository, IParecerPdfFactory parecerPdfFactory) : base(notification)
    {
        _logger = logger;
        _candidatoRepository = candidatoRepository;
        _parecerPdfFactory = parecerPdfFactory;
    }

    [HttpGet("candidato/{id}")]
    public async Task<IActionResult> ObterParecerCandidato(Guid id, [FromQuery] string resp)
    {
        var candidato = await _candidatoRepository.GetById(id);

        var pdf = _parecerPdfFactory.Gerar(candidato, resp);

        return CustomResponse(pdf);
    }
}
