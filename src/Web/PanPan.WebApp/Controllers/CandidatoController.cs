using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;
using PanPan.WebApp.HttpServices;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.Controllers;

[Route("candidatos")]
public class CandidatoController : Controller
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoHttpService _candidatoHttpService;
    private readonly INotificationHandler _notification;
    private readonly IMapper _mapper;
    public CandidatoController(ILogger<CandidatoController> logger, ICandidatoHttpService candidatoHttpService, IMapper mapper, INotificationHandler notification)
    {
        _logger = logger;
        _candidatoHttpService = candidatoHttpService;
        _mapper = mapper;
        _notification = notification;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index(string nome)
    {
        var response = await _candidatoHttpService.ObterPaginado(new() { Nome = nome });

        var model = _mapper.Map<CandidatosViewModel>(response);

        model.Nome = nome;

        return View(model);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Index(CandidatosViewModel request)
    {
        var response = await _candidatoHttpService.ObterPaginado(request);

        var model = _mapper.Map<CandidatosViewModel>(response);

        model.Status = request.Status;
        model.Nome = request.Nome;
        model.Estado = request.Estado;
        model.Codigo = request.Codigo;
        model.Bairro = request.Bairro;
        model.Cidade = request.Cidade;
        model.Celular = request.Celular;
        model.CelularAlternativo = request.CelularAlternativo;
        model.Telefone = request.Telefone;
        model.Email = request.Email;
        model.Sexo = request.Sexo;
        model.OrderBy = request.OrderBy;

        return PartialView("Partial/_Candidatos", model);
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

        if (_notification.HasNotifications())
            return View(model);

        return RedirectToAction(nameof(Index), new { nome = request.Nome });
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

        if (!_notification.HasNotifications())
            TempData["Sucesso"] = "Candidato alterado com sucesso!";

        return View(model);
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
    [Route("{candidatoId}/formacoes")]
    public async Task<IActionResult> Formacoes(Guid candidatoId)
    {
        var response = await _candidatoHttpService.ObterFormacoes(candidatoId);

        var model = _mapper.Map<IEnumerable<FormacaoViewModel>>(response);

        return PartialView(model);
    }

    [HttpPost]
    [Route("formacao/incluir")]
    public async Task<IActionResult> IncluirFormacao(FormacaoViewModel model)
    {
        var request = _mapper.Map<AddFormacaoRequest>(model);

        await _candidatoHttpService.Incluir(model.CandidatoId, request);

        return PartialView(nameof(IncluirFormacao), model.CandidatoId);
    }

    [HttpPost]
    [Route("formacao/alterar")]
    public async Task<IActionResult> AlterarFormacao(FormacaoViewModel model)
    {
        var request = _mapper.Map<UpdateFormacaoRequest>(model);

        await _candidatoHttpService.Alterar(model.CandidatoId, model.Id, request);

        if (!_notification.HasNotifications())
            TempData["Sucesso"] = "Formacao alterada com sucesso!";

        return PartialView(nameof(AlterarFormacao), model);
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
    [Route("{candidatoId}/experiencias")]
    public async Task<IActionResult> Experiencias(Guid candidatoId)
    {
        var response = await _candidatoHttpService.ObterExperiencias(candidatoId);

        var model = _mapper.Map<IEnumerable<ExperienciaViewModel>>(response);

        return PartialView(model);
    }

    [HttpPost]
    [Route("experiencia/incluir")]
    public async Task<IActionResult> IncluirExperiencia(ExperienciaViewModel model)
    {
        var request = _mapper.Map<AddExperienciaRequest>(model);

        await _candidatoHttpService.Incluir(model.CandidatoId, request);

        return PartialView(nameof(IncluirExperiencia), model.CandidatoId);
    }
    [HttpPost]
    [Route("experiencia/alterar")]
    public async Task<IActionResult> AlterarExperiencia(ExperienciaViewModel model)
    {
        var request = _mapper.Map<UpdateExperienciaRequest>(model);

        await _candidatoHttpService.Alterar(model.CandidatoId, model.Id, request);

        if (!_notification.HasNotifications())
            TempData["Sucesso"] = "Experiencia alterada com sucesso!";

        return PartialView("Partial/_Experiencia", model);
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
        var pdf = await _candidatoHttpService.GerarParecer(id);

        return new JsonResult(pdf);
    }
}
