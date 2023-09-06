using Api.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using Vagas.Api.Dto.Request;
using Vagas.Business.Interfaces.Repositories;
using Vagas.Business.Models;

namespace Vagas.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/vaga")]
public class VagaController : MainController
{
    private readonly ILogger<VagaController> _logger;
    private readonly IVagaRepository _vagaRepository;

    public VagaController(ILogger<VagaController> logger, IVagaRepository vagaRepository)
    {
        _logger = logger;
        _vagaRepository = vagaRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Get() => CustonResponse(await _vagaRepository.ObterTodos());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => CustonResponse(await _vagaRepository.ObterPorId(id));

    [HttpPost]
    public async Task<IActionResult> Post(AddVagaRequest request)
    {
        var vaga = new Vaga(request.Nome, request.Descricao, request.ClienteId);

        _vagaRepository.Adicionar(vaga);

        if (!await _vagaRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, UpdatteVagaRequest request)
    {
        var vaga = await _vagaRepository.ObterPorId(id);

        if (vaga is null) return CustonResponse();

        vaga.Atualizar(request.Nome, request.Descricao);

        _vagaRepository.Atualizar(vaga);

        if (!await _vagaRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vaga = await _vagaRepository.ObterPorId(id);

        if (vaga is null) return CustonResponse();

        _vagaRepository.Remover(vaga);

        if (!await _vagaRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }
}