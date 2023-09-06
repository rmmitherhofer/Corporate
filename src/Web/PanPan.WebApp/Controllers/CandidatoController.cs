using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PanPan.WebApp.HttpServices;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.Controllers;

[Route("candidatos")]
public class CandidatoController : Controller
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoHttpService _candidatoHttpService;
    private readonly IMapper _mapper;
    public CandidatoController(ILogger<CandidatoController> logger, ICandidatoHttpService candidatoHttpService, IMapper mapper)
    {
        _logger = logger;
        _candidatoHttpService = candidatoHttpService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("")]
    [Route("index")]
    public async Task<IActionResult> Index()
    {
        var response = await _candidatoHttpService.ObterTodos();

        var model = _mapper.Map<IEnumerable<CandidatoViewModel>>(response);

        return View(model);
    }

    #region Candidato

    [HttpGet]
    [Route("incluir")]
    public IActionResult IncluirCandidato()
    {
        return View();
    }
    [HttpPost]
    [Route("incluir")]
    public async Task<IActionResult> IncluirCandidato(CandidatoViewModel model)
    {
        var request = _mapper.Map<AddCandidatoRequest>(model);

        await _candidatoHttpService.Incluir(request);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    [Route("alterar/{id}")]
    public async Task<IActionResult> AlterarCandidato(Guid id)
    {
        var response = await _candidatoHttpService.ObterPorId(id);

        var model = _mapper.Map<CandidatoViewModel>(response);

        return View(model);
    }
    [HttpPost]
    [Route("alterar/{id}")]
    public async Task<IActionResult> AlterarCandidato(Guid id, CandidatoViewModel model)
    {
        var request = _mapper.Map<UpdateCandidatoRequest>(model);

        await _candidatoHttpService.Alterar(id, request);

        return RedirectToAction(nameof(AlterarCandidato), new { Id = id });
    }
    [HttpGet]
    [Route("{id}/remover")]
    public async Task<IActionResult> RemoverCandidato(Guid id)
    {
        await _candidatoHttpService.Remover(id);

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Formacao
    [HttpGet]
    [Route("{id}/formacao/incluir")]
    public async Task<IActionResult> IncluirFormacao(Guid id)
    {
        return View(new FormacaoViewModel() { CandidatoId = id });
    }
    [HttpPost]
    [Route("{id}/formacao/incluir")]
    public async Task<IActionResult> IncluirFormacao(Guid id, FormacaoViewModel model)
    {
        var request = _mapper.Map<AddFormacaoRequest>(model);

        await _candidatoHttpService.Incluir(id, request);

        return RedirectToAction(nameof(AlterarCandidato), new { Id = id });
    }
    [HttpGet]
    [Route("{id}/formacao/alterar/{formacaoId}")]
    public async Task<IActionResult> AlterarFormacao(Guid id, Guid formacaoId)
    {
        var response = await _candidatoHttpService.ObterPorId(id);

        var model = response.Formacoes.Where(f => f.Id == formacaoId).FirstOrDefault();

        return View(_mapper.Map<FormacaoViewModel>(model));
    }
    [HttpPost]
    [Route("{id}/formacao/alterar/{formacaoId}")]
    public async Task<IActionResult> AlterarFormacao(Guid id, Guid formacaoId, FormacaoViewModel model)
    {
        var request = _mapper.Map<UpdateFormacaoRequest>(model);

        await _candidatoHttpService.Alterar(id, formacaoId, request);

        return RedirectToAction(nameof(AlterarCandidato), new { Id = id });
    }
    [HttpGet]
    [Route("{id}/formacao/remover/{formacaoId}")]
    public async Task<IActionResult> RemoverFormacao(Guid id, Guid formacaoId)
    {
        await _candidatoHttpService.RemoverFormacao(id, formacaoId);

        return RedirectToAction(nameof(AlterarCandidato), new { Id = id });
    }
    #endregion

    #region Experiencia profissional
    [HttpGet]
    [Route("{id}/experiencia/incluir")]
    public async Task<IActionResult> IncluirExperiencia(Guid id)
    {
        return View(new ExperienciaViewModel() { CandidatoId = id });
    }
    [HttpPost]
    [Route("{id}/experiencia/incluir")]
    public async Task<IActionResult> IncluirExperiencia(Guid id, ExperienciaViewModel model)
    {
        var request = _mapper.Map<AddExperienciaRequest>(model);

        await _candidatoHttpService.Incluir(id, request);

        return RedirectToAction(nameof(AlterarCandidato), new { Id = id });
    }
    [HttpGet]
    [Route("{id}/experiencia/alterar/{experienciaId}")]
    public async Task<IActionResult> AlterarExperiencia(Guid id, Guid experienciaId)
    {
        var response = await _candidatoHttpService.ObterPorId(id);

        var model = response.Experiencias.Where(f => f.Id == experienciaId).FirstOrDefault();

        return View(_mapper.Map<ExperienciaViewModel>(model));
    }
    [HttpPost]
    [Route("{id}/experiencia/alterar/{experienciaId}")]
    public async Task<IActionResult> AlterarExperiencia(Guid id, Guid experienciaId, ExperienciaViewModel model)
    {
        var request = _mapper.Map<UpdateExperienciaRequest>(model);

        await _candidatoHttpService.Alterar(id, experienciaId, request);

        return RedirectToAction(nameof(AlterarCandidato), new { Id = id });
    }
    [HttpGet]
    [Route("{id}/experiencia/remover/{experienciaId}")]
    public async Task<IActionResult> RemoverExperiencia(Guid id, Guid experienciaId)
    {
        await _candidatoHttpService.RemoverExperiencia(id, experienciaId);

        return RedirectToAction(nameof(AlterarCandidato), new { Id = id });
    }
    #endregion

    [HttpPost]
    [Route("{id}/gerar-parecer")]
    public async Task<IActionResult> GerarParecer(Guid id)
    {
        var pdf =  await _candidatoHttpService.GerarParecer(id);

        return new JsonResult(pdf);
    }
}
