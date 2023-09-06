using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PanPan.WebApp.HttpServices;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.Controllers;

public class ClienteController : Controller
{
    private readonly ILogger<ClienteController> _logger;
    private readonly IClienteHttpService _clienteHttpService;
    private readonly IMapper _mapper;

    public ClienteController(ILogger<ClienteController> logger, IClienteHttpService clienteHttpService, IMapper mapper)
    {
        _logger = logger;
        _clienteHttpService = clienteHttpService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await _clienteHttpService.ObterTodos();

        return View(model);
    }

    [HttpGet]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> New(ClienteViewModel model)
    {
        AddClienteRequest request = new()
        {
            Cnpj = model.Cnpj,
            NomeFantasia = model.NomeFantasia,
            NomeRepresentante = model.NomeRepresentante,
            Telefone = model.Telefone,
            Email = model.Email
        };

        await _clienteHttpService.Adicionar(request);

        return RedirectToAction(nameof(Index));
    }
}