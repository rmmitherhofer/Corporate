using Candidatos.Business.Enums;
using Core.DomainObjects;
using Exceptions;
using System.Text.Json.Serialization;

namespace Candidatos.Business.Models;

public class Formacao : Entity
{
    public string NomeInstituicao { get; private set; } = string.Empty;
    public string? Descricao { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public TipoFormacao Tipo { get; private set; }
    public Ensino? Ensino { get; private set; }
    public Curso? Curso { get; private set; }
    public StatusFormacao Status { get; private set; }
    public PeriodoFormacao Periodo { get; private set; }


    [JsonIgnore]
    public Candidato Candidato { get; protected set; }
    public Guid CandidatoId { get; private set; }

    protected Formacao() { }

    public Formacao(Guid candidatoId, string nomeInstituicao, DateTime? dataInicio, DateTime? dataFim, TipoFormacao tipo, StatusFormacao status, Curso? curso, string? descricao, Ensino? ensino, PeriodoFormacao periodo)
    {
        Tipo = tipo;

        AtribuirNome(nomeInstituicao);

        AtribuirDescricao(descricao);

        CandidatoId = candidatoId;

        Status = status;

        Periodo = periodo;

        IncluirPeriodo(dataInicio, dataFim);
        AtribuirCurso(curso, descricao);
        AtribuirEnsino(ensino);
    }

    public void Alterar(string nomeInstituicao, DateTime? dataInicio, DateTime? dataFim, StatusFormacao status, Curso? curso, string? descricao, Ensino? ensino, PeriodoFormacao periodo)
    {
        Status = status;

        AtribuirNome(nomeInstituicao);

        AtribuirDescricao(descricao);

        Periodo = periodo;

        IncluirPeriodo(dataInicio, dataFim);
        AtribuirCurso(curso, descricao);
        AtribuirEnsino(ensino);
    }

    private void AtribuirNome(string nomeInstituicao)
    {
        if (Tipo == TipoFormacao.Curso && string.IsNullOrEmpty(nomeInstituicao))
            throw new DomainException("O nome da instituicao deve ser informado.");

        NomeInstituicao = nomeInstituicao.Trim();
    }
    private void AtribuirDescricao(string? descricao)
    {
        if (Tipo == TipoFormacao.Curso && string.IsNullOrEmpty(descricao))
            throw new DomainException("A descricao do curso deve ser informada.");

        if (Tipo == TipoFormacao.Ensino && Ensino > Enums.Ensino.Medio && string.IsNullOrEmpty(descricao))
            throw new DomainException("A descricao do ensino deve ser informada quando superior ao nivel médio.");

        Descricao = descricao?.Trim();
    }

    private void AtribuirCurso(Curso? curso, string? descricao)
    {
        if (Tipo != TipoFormacao.Curso) return;

        if (curso is null) throw new DomainException("O tipo de curso deve ser selecionado.");

        Curso = curso;
    }

    private void AtribuirEnsino(Ensino? ensino)
    {
        if (Tipo != TipoFormacao.Ensino) return;

        if (ensino is null) throw new DomainException("O tipo de ensino deve ser selecionado.");

        Ensino = ensino;
    }

    private void IncluirPeriodo(DateTime? dataInicio, DateTime? dataFim)
    {
        DataInicio = dataInicio;

        DataFim = dataFim;
    }
}
