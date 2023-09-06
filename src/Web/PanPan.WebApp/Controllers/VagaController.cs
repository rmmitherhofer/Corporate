using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PanPan.WebApp.HttpServices;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.Controllers;

public class VagaController : Controller
{
    private readonly ILogger<VagaController> _logger;
    private readonly IVagaHttpService _vagaHttpService;
    private readonly IMapper _mapper;

    public VagaController(ILogger<VagaController> logger, IVagaHttpService vagaHttpService, IMapper mapper)
    {
        _logger = logger;
        _vagaHttpService = vagaHttpService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await _vagaHttpService.ObterTodos();

        return View(model);
    }

    [HttpGet]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> New(VagaViewModel model)
    {
        AddVagaRequest request = new()
        {
            Nome = model.Nome,
            Descricao = model.Descricao,
            ClienteId = model.ClienteId
        };

        await _vagaHttpService.Adicionar(request);

        return RedirectToAction(nameof(Index));
    }
}