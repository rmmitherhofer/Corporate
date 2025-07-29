using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PanPan.WebApp.Enums;
using PanPan.WebApp.Helpers;
using PanPan.WebApp.HttpServices;
using PanPan.WebApp.Models.Request.Candidato;
using PanPan.WebApp.Models.Response.NewFolder;
using PanPan.WebApp.Models.ViewModels.Candidatos;
using System.Reflection;
using System.Text.Json;
using Zypher.Json;
using Zypher.Notifications.Interfaces;
using Zypher.Notifications.Messages;

namespace PanPan.WebApp.Controllers;

[Route("candidatos")]
public class CandidatoController : Controller
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoHttpService _candidatoHttpService;
    private readonly ITalentMindHttpService _talentMindHttpService;
    private readonly INotificationHandler _notification;
    private readonly IMapper _mapper;
    public CandidatoController(ILogger<CandidatoController> logger, ICandidatoHttpService candidatoHttpService, IMapper mapper, INotificationHandler notification, ITalentMindHttpService talentMindHttpService)
    {
        _logger = logger;
        _candidatoHttpService = candidatoHttpService;
        _mapper = mapper;
        _notification = notification;
        _talentMindHttpService = talentMindHttpService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index(string nome)
    {
        var response = await _candidatoHttpService.ObterPaginado(new() { Nome = nome, OrderBy = "-registrationDate" });

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
        return View(new CandidatoViewModel());
    }
    [HttpPost]
    [Route("incluir")]
    public async Task<IActionResult> IncluirCandidato(CandidatoViewModel model)
    {
        var request = _mapper.Map<AddCandidatoRequest>(model);

        await _candidatoHttpService.Adicionar(request);

        if (_notification.HasNotifications())
            return View(model);

        return RedirectToAction(nameof(Index), new { nome = request.Nome });
    }
    [HttpGet]
    [Route("{id}/alterar")]
    public async Task<IActionResult> AlterarCandidato(Guid id)
    {
        var response = await _candidatoHttpService.ObterPorId(id);

        var model = _mapper.Map<CandidatoViewModel>(response);

        return View(model);
    }
    [HttpPost]
    [Route("{id}/alterar")]
    public async Task<IActionResult> AlterarCandidato(Guid id, CandidatoViewModel model)
    {
        var request = _mapper.Map<UpdateCandidatoRequest>(model);

        await _candidatoHttpService.Alterar(id, request);

        if (!_notification.HasNotifications())
            TempData["Sucesso"] = "Candidato alterado com sucesso!";

        return View(model);
    }
    [HttpPost]
    [Route("{id}/remover")]
    public async Task<IActionResult> RemoverCandidato(Guid id)
    {
        await _candidatoHttpService.Remover(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Route("importar-curriculo")]
    public async Task<IActionResult> ImportarCurriculo(CandidatoViewModel model)
    {
        var response = await _talentMindHttpService.ExtractFromPdf(model.File);

        var resume = response.Resume;

        if (_notification.HasNotifications())
            return View(model);        

        return View(nameof(ResumeImportPreview), ParseToViewModel(resume));
    }

    [HttpGet]
    [Route("cadatro-curriculo")]
    public async Task<IActionResult> ResumeImportPreview(CandidatoViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    [Route("cadatro-curriculo")]
    public async Task<IActionResult> CadastarCurriculo(CandidatoViewModel model)
    {
        var candidatoRequest = _mapper.Map<AddCandidatoRequest>(model);

        await _candidatoHttpService.Adicionar(candidatoRequest);

        var candidato = await _candidatoHttpService.ObterPaginado(new() { Nome = candidatoRequest.Nome });

        if (candidato.PageCount <= 0)
            return View(nameof(ResumeImportPreview), model);

        foreach (var formacaoRequest in _mapper.Map<List<AddFormacaoRequest>>(model.Formacoes))
            await _candidatoHttpService.Adicionar(candidato.Candidatos.FirstOrDefault().Id, formacaoRequest);

        foreach (var experienciaRequest in _mapper.Map<List<AddExperienciaRequest>>(model.Experiencias))
            await _candidatoHttpService.Adicionar(candidato.Candidatos.FirstOrDefault().Id, experienciaRequest);

        return RedirectToAction(nameof(AlterarCandidato), new { id = candidato.Candidatos.FirstOrDefault().Id });

    }

    [HttpGet]
    [Route("importar-curriculo-json")]
    public async Task<IActionResult> JsonResumeImport()
    {
        return View(new JsonResumeImport());
    }

    [HttpPost]
    [Route("importar-curriculo-json")]
    public async Task<IActionResult> JsonResumeImport(JsonResumeImport model)
    {
        if (string.IsNullOrWhiteSpace(model.Json))
        {
            _notification.Notify(new Notification("Json", "JSON não pode ser vazio."));
            return View(model);
        }

        ParserHttpResponse parsed;
        try
        {
            parsed = JsonExtensions.Deserialize<ParserHttpResponse>(model.Json);
        }
        catch (Exception ex)
        {
            _notification.Notify(new Notification("Json", $"JSON inválido: {ex.Message}"));
            return View(model);
        }

        if (parsed?.Resume is null)
        {
            _notification.Notify(new Notification("Json", "Estrutura do JSON não corresponde ao formato esperado."));
            return View(model);
        }
        return View(nameof(ResumeImportPreview), ParseToViewModel(parsed.Resume));
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

        await _candidatoHttpService.Adicionar(model.CandidatoId, request);

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

        return PartialView("Partial/_Formacao", model);
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

        await _candidatoHttpService.Adicionar(model.CandidatoId, request);

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


    private CandidatoViewModel ParseToViewModel(ResumeHttpResponse resume)
    {
        CandidatoViewModel model = new();

        var enderecoCorrigido = new EnderecoCorretor().CorrigirEndereco(resume.Address.State, resume.Address.City, resume.Address.Bairro);

        string telefone = null;

        string celular = null;

        var (tipo, numero) = TelefoneHelper.PadronizarTelefone(resume.Registration.PhoneNumber);
        if (tipo == TipoTelefone.Celular)
            celular = numero;
        else
            telefone = numero;

        var (tipo2, numero2) = TelefoneHelper.PadronizarTelefone(resume.Registration.MobileNumber);
        if (tipo2 == TipoTelefone.Celular)
            celular = numero2;
        else
            telefone = numero2;

        model.Nome = resume.Registration.FullName;
        model.Sexo = SexoExtensions.ParseOuInferirSexo(resume.Registration.Gender, resume.Registration.FullName);
        model.Telefone = telefone;
        model.Celular = celular;
        model.Email = resume.Registration.Email;
        model.DataNascimento = DataNascimentoExtensions.TryParseBirthDate(resume.Registration.BrithDate.ToString())?.ToString("dd/MM/yyyy");
        model.Idade = IdadeHelper.MapearIdade(resume.Registration.Age);
        model.Naturalidade = null;
        model.StatusCivil = EstadoCivilHelper.MapearEstadoCivil(resume.Registration.MaterialStatus);
        model.CNH = CNHNormalizer.NormalizeCNH(resume.Registration.CNH);
        model.Comentarios = resume.Registration.Goals;
        model.Endereco = new()
        {
            Logradouro = resume.Address.Street,
            Numero = resume.Address.Number,
            Complemento = resume.Address.Complement,
            Cep = null,
            Bairro = enderecoCorrigido.Bairro,
            Cidade = enderecoCorrigido.Cidade,
            Estado = enderecoCorrigido.Estado,
        };

        model.Experiencias ??= [];
        model.Formacoes ??= [];
        foreach (var item in resume.Experiences)
        {
            var dataFim = DateHelper.TryParseToFirstDayOfMonth(item.EndDate);
            model.Experiencias.Add(new()
            {
                Empresa = item.Company,
                Cargo = item.Cargo,
                Atividade = item.Description,
                DataInicioString = DateHelper.TryParseToFirstDayOfMonth(item.StartDate)?.ToString("MM/yyyy"),
                DataFimString = (dataFim > DateTime.Now ? DateTime.Now : dataFim)?.ToString("MM/yyyy"),
            });
        }

        foreach (var item in resume.Formations)
        {
            var dataFim = DateHelper.TryParseToFirstDayOfMonth(item.EndDate);
            model.Formacoes.Add(new()
            {
                NomeInstituicao = item.InstitutionName,
                Descricao = item.Description,
                Tipo = TipoFormacao.Ensino,
                DataInicioString = DateHelper.TryParseToFirstDayOfMonth(item.StartDate)?.ToString("MM/yyyy"),
                DataFimString = (dataFim > DateTime.Now ? DateTime.Now : dataFim)?.ToString("MM/yyyy"),
                Ensino = CourseClassifier.ClassificarEnsino(item.InstitutionName, item.Description)
            });
        }

        foreach (var item in resume.Courses)
        {
            var dataFim = DateHelper.TryParseToFirstDayOfMonth(item.EndDate);
            model.Formacoes.Add(new()
            {
                NomeInstituicao = item.InstitutionName,
                Descricao = item.Description,
                Tipo = TipoFormacao.Curso,
                DataInicioString = DateHelper.TryParseToFirstDayOfMonth(item.StartDate)?.ToString("MM/yyyy"),
                DataFimString = (dataFim > DateTime.Now ? DateTime.Now : dataFim)?.ToString("MM/yyyy"),
                Curso = CourseClassifier.ClassificarCurso(item.InstitutionName, item.Description)
            });
        }

        return model;
    }
}
