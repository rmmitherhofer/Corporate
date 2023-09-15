using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;
using PanPan.WebApp.HttpServices;
using PanPan.WebApp.Models.Request.Cliente;
using PanPan.WebApp.Models.ViewModels.Clientes;
using System.Diagnostics;

namespace PanPan.WebApp.Controllers;

[Route("clientes")]
public class ClienteController : Controller
{
    private readonly ILogger<ClienteController> _logger;
    private readonly IClienteHttpService _clienteHttpService;
    private readonly INotificationHandler _notification;
    private readonly IMapper _mapper;

    public ClienteController(ILogger<ClienteController> logger, IClienteHttpService clienteHttpService, IMapper mapper, INotificationHandler notification)
    {
        _logger = logger;
        _clienteHttpService = clienteHttpService;
        _mapper = mapper;
        _notification = notification;
    }

    #region Index

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var response = await _clienteHttpService.ObterPaginado(new());

        var model = _mapper.Map<ClientesViewModel>(response);

        return View(model);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Index(ClientesViewModel request)
    {
        var response = await _clienteHttpService.ObterPaginado(request);

        var model = _mapper.Map<ClientesViewModel>(response);

        model.Cnpj = request.Cnpj;
        model.Representante = request.Representante;
        model.Nome = request.Nome;
        model.Celular = request.Celular;
        model.Telefone = request.Telefone;
        model.Email = request.Email;
        model.Estado = request.Estado;
        model.Cidade = request.Cidade;
        model.OrderBy = request.OrderBy;

        return PartialView("Partial/_Clientes", model);
    }

    #endregion

    #region Empresa
    [HttpGet]
    [Route("incluir")]
    public IActionResult Incluir()
    {
        return View(new ClienteViewModel());
    }

    [HttpPost]
    [Route("incluir")]
    public async Task<IActionResult> Incluir(ClienteViewModel model)
    {
        var request = _mapper.Map<AddClienteRequest>(model);

        await _clienteHttpService.Adicionar(request);

        if (_notification.HasNotifications())
            return View(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("{id}/alterar")]
    public async Task<IActionResult> Alterar(Guid id)
    {
        var response = await _clienteHttpService.ObterPorId(id);

        var model = _mapper.Map<ClienteViewModel>(response);

        return View(model);
    }
    [HttpPost]
    [Route("{id}/alterar")]
    public async Task<IActionResult> Alterar(Guid id, ClienteViewModel model)
    {
        var request = _mapper.Map<UpdateClienteRequest>(model);

        await _clienteHttpService.Alterar(id, request);

        if (!_notification.HasNotifications())
            TempData["Sucesso"] = "Cliente alterado com sucesso!";

        return View(model);
    }
    [HttpGet]
    [Route("{id}/remover")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _clienteHttpService.Remover(id);

        return RedirectToAction(nameof(Index));
    }
    #endregion
}