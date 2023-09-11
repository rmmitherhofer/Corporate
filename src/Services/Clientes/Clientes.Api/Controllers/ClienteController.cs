using Api.Service.Controllers;
using Clientes.Api.Dto.Request;
using Clientes.Business.Interfaces.Repositories;
using Clientes.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;

namespace Clientes.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/cliente")]
public class ClienteController : MainController
{
    private readonly ILogger<ClienteController> _logger;
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(INotificationHandler notification, ILogger<ClienteController> logger, IClienteRepository clienteRepository) : base(notification)
    {
        _logger = logger;
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => CustomResponse(await _clienteRepository.ObterTodos());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => CustomResponse(await _clienteRepository.ObterPorId(id));

    [HttpPost]
    public async Task<IActionResult> Post(AddClienteRequest request)
    {
        var vaga = new Cliente(request.Cnpj, request.NomeFantasia, request.NomeRepresentante, request.Telefone, request.Email);

        _clienteRepository.Adicionar(vaga);

        if (!await _clienteRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustomResponse();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, UpdateClienteRequest request)
    {
        var vaga = await _clienteRepository.ObterPorId(id);

        if (vaga is null) return CustomResponse();

        vaga.Atualizar(request.NomeFantasia, request.NomeRepresentante, request.Telefone, request.Email);

        _clienteRepository.Atualizar(vaga);

        if (!await _clienteRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustomResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vaga = await _clienteRepository.ObterPorId(id);

        if (vaga is null) return CustomResponse();

        _clienteRepository.Remover(vaga);

        if (!await _clienteRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustomResponse();
    }
}