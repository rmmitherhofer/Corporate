using Api.Service.Controllers;
using Candidatos.Api.DTO.Request;
using Candidatos.Business.Interfaces.Repositories;
using Candidatos.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Candidatos.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/candidato")]
public class CandidatoController : MainController
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoRepository _candidatoRepository;

    public CandidatoController(ILogger<CandidatoController> logger, ICandidatoRepository candidatoRepository)
    {
        _logger = logger;
        _candidatoRepository = candidatoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodosCandidatos()
    {
        var candidatos = await _candidatoRepository.ObterTodos();

        return CustonResponse(candidatos.OrderBy(c => c.Codigo));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterCandidadoPorId(Guid id) => CustonResponse(await _candidatoRepository.ObterPorId(id));

    [HttpPost]
    public async Task<IActionResult> IncluirCandidato(AddCandidatoRequest request)
    {
        var candidato = new Candidato(request.Nome, request.Sexo, request.RecrutadoPor, request.Telefone, request.Celular, request.CelularAlternativo, request.Email);

        candidato.IncluirEndereco(new(request.Endereco.Logradouro,
            request.Endereco.Numero,
            request.Endereco.Complemento,
            request.Endereco.Bairro,
            request.Endereco.Cep,
            request.Endereco.Cidade,
            request.Endereco.Estado,
            request.Endereco.Domicilio,
            request.Endereco.TipoDomicilio,
            request.Endereco.MoraCom));

        _candidatoRepository.Adicionar(candidato);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AlterarCandidato(Guid id, UpdateCandidatoRequest request)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        candidato.AtribuirNome(request.Nome);

        candidato.AlterarApelido(request.Apelido);

        candidato.AtribuirDadosContato(request.Telefone, request.Celular, request.CelularAlternativo, request.Email);

        candidato.AtribuirDadosPessoais(request.DataNascimento, request.Idade, request.Naturalidade, request.StatusCivil);

        candidato.AtribuirDadosComplementares(request.Idiomas, request.Conhecimentos, request.PretensaoSalarial, request.IdadeFilhos, request.QuantidadeFilhos);

        candidato.IncluirRelatos(request.RelatoCandidato, request.RelatoEntrevistador);

        candidato.IncluirComentarios(request.Comentarios);

        candidato.AlterarStatus(request.Status);

        candidato?.AlterarEndereco(
            new(request.Endereco.Logradouro,
            request.Endereco.Numero,
            request.Endereco.Complemento,
            request.Endereco.Bairro,
            request.Endereco.Cep,
            request.Endereco.Cidade,
            request.Endereco.Estado,
            request.Endereco.Domicilio,
            request.Endereco.TipoDomicilio,
            request.Endereco.MoraCom));

        _candidatoRepository.Atualizar(candidato);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCandidato(Guid id)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        _candidatoRepository.Remover(candidato);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpPost("{id}/formacao")]
    public async Task<IActionResult> IncluirFormacao(Guid id, AddFormacaoRequest request)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        Formacao formacao = new(candidato.Id, request.NomeInstituicao, request.DataInicio, request.DataFim, request.Tipo, request.Status, request.Curso, request.Descricao, request.Ensino, request.Periodo);

        _candidatoRepository.Adicionar(formacao);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpPut("{id}/formacao/{formacaoId}")]
    public async Task<IActionResult> AlterarFormacao(Guid id, Guid formacaoId, UpdateFormacaoRequest request)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        var formacao = candidato.Formacoes.Where(f => f.Id == formacaoId).FirstOrDefault();

        if (formacao is null) return CustonResponse();

        formacao.Alterar(request.DataInicio, request.DataFim, request.Status, request.Curso, request.Descricao, request.Ensino, request.Periodo);

        _candidatoRepository.Atualizar(formacao);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }
    [HttpDelete("{id}/formacao/{formacaoId}")]
    public async Task<IActionResult> DeleteFormacao(Guid id, Guid formacaoId)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        var formacao = candidato.Formacoes.Where(f => f.Id == formacaoId).FirstOrDefault();

        if (formacao is null) return CustonResponse();

        _candidatoRepository.Remover(formacao);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpPost("{id}/experiencia")]
    public async Task<IActionResult> IncluirExperiencia(Guid id, AddExperienciaRequest request)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        Experiencia experiencia = new(candidato.Id,
            request.Empresa,
            request.Cargo,
            request.RegimeContratacao,
            request.Atividade,
            request.DataInicio,
            request.Observacoes,
            request.Habilidade,
            request.UltimoSalario,
            request.DataFim,
            request.MotivoDesligamento,
            request.Status);

        _candidatoRepository.Adicionar(experiencia);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }
    [HttpPut("{id}/experiencia/{experienciaId}")]
    public async Task<IActionResult> AlterarExperiencia(Guid id, Guid experienciaId, UpdateExperienciaRequest request)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        var experiencia = candidato.Experiencias.Where(f => f.Id == experienciaId).FirstOrDefault();

        if (experiencia is null) return CustonResponse();

        experiencia.Alterar(request.Cargo,
            request.Atividade,
            request.RegimeContratacao,
            request.DataInicio,
            request.DataFim,
            request.MotivoDesligamento,
            request.Observacoes,
            request.Habilidade,
            request.UltimoSalario,
            request.Status);

        _candidatoRepository.Atualizar(experiencia);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }
    [HttpDelete("{id}/experiencia/{experienciaId}")]
    public async Task<IActionResult> RemoverExperiencia(Guid id, Guid experienciaId)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        var experiencia = candidato.Experiencias.Where(f => f.Id == experienciaId).FirstOrDefault();

        if (experiencia is null) return CustonResponse();

        _candidatoRepository.Remover(experiencia);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpPut("{id}/tag")]
    public async Task<IActionResult> AlterarTag(Guid id, [FromBody] string request)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        candidato.IncluirTag(request);

        _candidatoRepository.Atualizar(candidato);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

    [HttpDelete("{id}/tag")]
    public async Task<IActionResult> RemoverTag(Guid id, [FromBody] string request)
    {
        var candidato = await _candidatoRepository.ObterPorId(id);

        if (candidato is null) return CustonResponse();

        candidato.RemoverTag(request);

        _candidatoRepository.Atualizar(candidato);

        if (!await _candidatoRepository.UnitOfWork.Commit())
            return BadRequest("Error");

        return CustonResponse();
    }

}
