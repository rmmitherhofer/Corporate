using Api.Service.Controllers;
using Candidatos.Business.Factories;
using Candidatos.Business.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Candidatos.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/parecer")]
public class ParecerController : MainController
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IParecerPdfFactory _parecerPdfFactory;

    public ParecerController(ILogger<CandidatoController> logger, ICandidatoRepository candidatoRepository, IParecerPdfFactory parecerPdfFactory)
    {
        _logger = logger;
        _candidatoRepository = candidatoRepository;
        _parecerPdfFactory = parecerPdfFactory;
    }

    [HttpGet("candidato/{id}")]
    public async Task<IActionResult> ObterParecerCandidato(Guid id)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        var pdf = _parecerPdfFactory.Gerar(candidato);

        return CustonResponse(pdf);
    }
}
