using Api.Service.Controllers;
using Clientes.Api.Dto.Request;
using Clientes.Business.Interfaces.Repositories;
using Clientes.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/cliente")]
public class ClienteController : MainController
{
    private readonly ILogger<ClienteController> _logger;
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository)
    {
        _logger = logger;
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => CustonResponse(await _clienteRepository.ObterTodos());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => CustonResponse(await _clienteRepository.ObterPorId(id));

    [HttpPost]
    public async Task<IActionResult> Post(AddClienteRequest request)
    {
        var vaga = new Cliente(request.Cnpj, request.NomeFantasia, request.NomeRepresentante, request.Telefone, request.Email);

        _clienteRepository.Adicionar(vaga);

        if (!await _clienteRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, UpdateClienteRequest request)
    {
        var vaga = await _clienteRepository.ObterPorId(id);

        if (vaga is null) return CustonResponse();

        vaga.Atualizar(request.NomeFantasia, request.NomeRepresentante, request.Telefone, request.Email);

        _clienteRepository.Atualizar(vaga);

        if (!await _clienteRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vaga = await _clienteRepository.ObterPorId(id);

        if (vaga is null) return CustonResponse();

        _clienteRepository.Remover(vaga);

        if (!await _clienteRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }
}