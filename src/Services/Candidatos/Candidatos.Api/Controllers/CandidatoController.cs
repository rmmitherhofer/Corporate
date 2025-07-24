using AutoMapper;
using Candidatos.Api.DTO.Request;
using Candidatos.Api.DTO.Responses;
using Candidatos.Business.Filters;
using Candidatos.Business.Interfaces.Repositories;
using Candidatos.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zypher.Api.Foundation.Controllers;
using Zypher.Notifications.Interfaces;
using Zypher.Responses;

namespace Candidatos.Api.Controllers;

[ApiVersion("1.0", Deprecated = false)]
[Route("api/v{version:ApiVersion}/candidato")]
public class CandidatoController : MainController
{
    private readonly ILogger<CandidatoController> _logger;
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IMapper _mapper;

    public CandidatoController(ILogger<CandidatoController> logger,
        INotificationHandler notification,
        ICandidatoRepository candidatoRepository,
        IMapper mapper) : base(notification)
    {
        _logger = logger;
        _candidatoRepository = candidatoRepository;
        _mapper = mapper;
    }

    #region Candidato
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Candidato>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ObterTodosCandidatos()
    {
        var candidatos = await _candidatoRepository.GetAll();

        return CustomResponse(candidatos.OrderBy(c => c.Codigo));
    }

    [HttpGet("paginado")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CandidatosPaginatedResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ObterPaginado([FromQuery] CandidatoFilterRequest request)
    {
        var (candidatos, pageCount, totalRecords) = await _candidatoRepository.ObterPaginado(_mapper.Map<CandidatoFilter>(request));

        CandidatosPaginatedResponse response = new(totalRecords, request.PageNumber, pageCount, candidatos);

        return CustomResponse(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Candidato))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ObterCandidadoPorId(Guid id)
        => CustomResponse(await _candidatoRepository.GetById(id));

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
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

        _candidatoRepository.Add(candidato);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> AlterarCandidato(Guid id, UpdateCandidatoRequest request)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify(nameof(id), $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        candidato.AtribuirNome(request.Nome);

        candidato.AtribuirSexo(request.Sexo);

        candidato.AlterarApelido(request.Apelido);

        candidato.AtribuirDadosContato(request.Telefone, request.Celular, request.CelularAlternativo, request.Email);

        candidato.AtribuirDadosPessoais(request.DataNascimento, request.Idade, request.Naturalidade, request.StatusCivil);

        candidato.AtribuirDadosComplementares(request.Idiomas, request.Conhecimentos, request.PretensaoSalarial, request.IdadeFilhos, request.QuantidadeFilhos);

        candidato.IncluirRelatos(request.RelatoCandidato, request.RelatoEntrevistador);

        candidato.IncluirComentarios(request.Comentarios);

        candidato.AlterarStatus(request.Status);

        candidato.AtribuirCNH(request.CNH);

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

        _candidatoRepository.Update(candidato);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteCandidato(Guid id)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify(nameof(id), $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        _candidatoRepository.Remove(candidato);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }
    #endregion

    #region Formacao
    [HttpGet("{candidatoId}/formacao")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CandidatosPaginatedResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ObterFormacoesPorCandidato(Guid candidatoId)
    {
        var formacoes = await _candidatoRepository.ObterFormacoesPorCandidatoId(candidatoId);

        return CustomResponse(formacoes?.OrderByDescending(c => c.DataFim));
    }

    [HttpPost("{id}/formacao")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> IncluirFormacao(Guid id, AddFormacaoRequest request)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify(nameof(id), $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        Formacao formacao = new(candidato.Id, request.NomeInstituicao, request.DataInicio, request.DataFim, request.Tipo, request.Status, request.Curso, request.Descricao, request.Ensino, request.Periodo);

        _candidatoRepository.Adicionar(formacao);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

    [HttpPut("{id}/formacao/{formacaoId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> AlterarFormacao(Guid id, Guid formacaoId, UpdateFormacaoRequest request)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify("put-formacao", $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        var formacao = candidato.Formacoes.Where(f => f.Id == formacaoId).FirstOrDefault();

        if (formacao is null)
        {
            Notify("put-formacao", $"Formacao {id} nao localizado.");
            return CustomResponse();
        }

        formacao.Alterar(request.NomeInstituicao, request.DataInicio, request.DataFim, request.Status, request.Curso, request.Descricao, request.Ensino, request.Periodo);

        _candidatoRepository.Atualizar(formacao);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

    [HttpDelete("{id}/formacao/{formacaoId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteFormacao(Guid id, Guid formacaoId)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify(nameof(id), $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        var formacao = candidato.Formacoes.Where(f => f.Id == formacaoId).FirstOrDefault();

        if (formacao is null)
        {
            Notify(nameof(formacaoId), $"Formacao {formacaoId} nao localizado.");
            return CustomResponse();
        }

        _candidatoRepository.Remover(formacao);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }
    #endregion

    #region Experiencia
    [HttpGet("{candidatoId}/experiencia")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CandidatosPaginatedResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ObterExperienciasPorCandidato(Guid candidatoId)
    {
        var experiencias = await _candidatoRepository.ObterExperienciasPorCandidatoId(candidatoId);

        return CustomResponse(experiencias?.OrderByDescending(c => c.DataFim));
    }

    [HttpPost("{id}/experiencia")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> IncluirExperiencia(Guid id, AddExperienciaRequest request)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify("id", $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        Experiencia experiencia = new(
            candidato.Id,
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

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }
    [HttpPut("{id}/experiencia/{experienciaId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> AlterarExperiencia(Guid id, Guid experienciaId, UpdateExperienciaRequest request)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify("put-experiencia", $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        var experiencia = candidato.Experiencias.Where(f => f.Id == experienciaId).FirstOrDefault();

        if (experiencia is null)
        {
            Notify("put-experiencia", $"Experiencia {id} nao localizada.");
            return CustomResponse();
        }

        experiencia.Alterar(
            request.Empresa,
            request.Cargo,
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

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }
    [HttpDelete("{id}/experiencia/{experienciaId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> RemoverExperiencia(Guid id, Guid experienciaId)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify("delete-experiencia", $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        var experiencia = candidato.Experiencias.FirstOrDefault(f => f.Id == experienciaId);

        if (experiencia is null)
        {
            Notify("delete-experiencia", $"Experiencia {id} nao localizada.");
            return CustomResponse();
        }

        _candidatoRepository.Remover(experiencia);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    } 
    #endregion

    [HttpPut("{id}/tag")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> AlterarTag(Guid id, [FromBody] string request)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify("put-tag", $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        candidato.IncluirTag(request);

        _candidatoRepository.Update(candidato);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

    [HttpDelete("{id}/tag")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> RemoverTag(Guid id, [FromBody] string request)
    {
        var candidato = await _candidatoRepository.GetById(id);

        if (candidato is null)
        {
            Notify("delete-tag", $"Candidato {id} nao localizado.");
            return CustomResponse();
        }

        candidato.RemoverTag(request);

        _candidatoRepository.Update(candidato);

        await _candidatoRepository.UnitOfWork.Commit();

        return CustomResponse();
    }

}
