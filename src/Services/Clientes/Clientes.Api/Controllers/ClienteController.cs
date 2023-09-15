using Api.Service.Controllers;
using AutoMapper;
using Clientes.Api.Dto.Request;
using Clientes.Api.Dto.Response;
using Clientes.Business.Filters;
using Clientes.Business.Interfaces.Repositories;
using Clientes.Business.Models;
using Core.ValueObjects;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Notifications.Interfaces;
using Responses;
using System.Net;

namespace Clientes.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/cliente")]
public class ClienteController : MainController
{
    private readonly ILogger<ClienteController> _logger;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteController(INotificationHandler notification, ILogger<ClienteController> logger, IClienteRepository clienteRepository, IMapper mapper) : base(notification)
    {
        _logger = logger;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Cliente>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Get() => CustomResponse(await _clienteRepository.ObterTodos());

    [HttpGet("paginado")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ClientesPaginatedResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ObterPaginado([FromQuery] ClienteFilterRequest request)
    {
        var (clientes, pageCount, totalRecords) = await _clienteRepository.ObterPaginado(_mapper.Map<ClienteFilter>(request));

        ClientesPaginatedResponse response = new(totalRecords, request.PageNumber, pageCount, clientes);

        return CustomResponse(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Cliente))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Get(Guid id) => CustomResponse(await _clienteRepository.ObterPorId(id));

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Post(AddClienteRequest request)
    {
        var cliente = await _clienteRepository.ObterPorCnpj(new Cnpj(request.Cnpj).Numero);

        if (cliente is not null)
        {
            Notificar(nameof(request.Cnpj), $"Cliente portador do CNPJ {request.Cnpj.MascaraCpfCnpj()} ja cadastrado na base de dados.");
            return CustomResponse();
        }

        cliente = new Cliente(request.Cnpj, request.Nome, request.Representante, request.Telefone, request.Email, request.Celular);

        cliente.IncluirEndereco(new(request.Endereco.Logradouro, request.Endereco.Numero, request.Endereco.Complemento, request.Endereco.Bairro, request.Endereco.Cep, request.Endereco.Cidade, request.Endereco.Estado));

        _clienteRepository.Adicionar(cliente);

        await _clienteRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Put(Guid id, UpdateClienteRequest request)
    {
        var cliente = await _clienteRepository.ObterPorId(id);

        if (cliente is null)
        {
            Notificar(nameof(id), $"Cliente {id} nao localizado.");
            return CustomResponse();
        }

        cliente.Atualizar(request.Nome, request.Representante, request.Telefone, request.Email, request.Celular);
        cliente.AlterarEndereco(new(request.Endereco.Logradouro, request.Endereco.Numero, request.Endereco.Complemento, request.Endereco.Bairro, request.Endereco.Cep, request.Endereco.Cidade, request.Endereco.Estado));

        _clienteRepository.Atualizar(cliente);

        await _clienteRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cliente = await _clienteRepository.ObterPorId(id);

        if (cliente is null)
        {
            Notificar(nameof(id), $"Cliente {id} nao localizado.");
            return CustomResponse();
        }

        _clienteRepository.Remover(cliente);

        await _clienteRepository.UnitOfWork.Commit();

        return CustomResponse();
    }
}