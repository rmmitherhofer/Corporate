using Candidatos.Business.Enums;
using Core.DomainObjects;
using System.Text.Json.Serialization;

namespace Candidatos.Business.Models;

public class Formacao : Entity
{
    public string NomeInstituicao { get; private set; } = string.Empty;
    public string? Descricao { get; private set; }
    public DateTime? DataInicio { get; private  set; }
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

        if (Tipo == TipoFormacao.Curso && string.IsNullOrEmpty(nomeInstituicao)) 
            throw new ArgumentNullException(nameof(nomeInstituicao), "O nome da instituicao deve ser informado.");

        if (Tipo == TipoFormacao.Curso && string.IsNullOrEmpty(descricao))
            throw new ArgumentNullException(nameof(descricao), "A descricao do curso deve ser informada.");

        CandidatoId = candidatoId;

        NomeInstituicao = nomeInstituicao.Trim();

        Status = status;

        Descricao = descricao?.Trim();

        Periodo = periodo;

        IncluirPeriodo(dataInicio, dataFim);
        AtribuirCurso(curso, descricao);
        AtribuirEnsino(ensino);
    }

    public void Alterar(DateTime? dataInicio, DateTime? dataFim, StatusFormacao status, Curso? curso, string? descricao, Ensino? ensino, PeriodoFormacao periodo)
    {
        Status = status;

        if (string.IsNullOrEmpty(descricao)) throw new ArgumentNullException(nameof(descricao), "A descricao do curso deve ser informada.");
        Descricao = descricao?.Trim();

        Periodo = periodo;

        IncluirPeriodo(dataInicio, dataFim);
        AtribuirCurso(curso, descricao);
        AtribuirEnsino(ensino);
    }

    private void AtribuirCurso(Curso? curso, string? descricao)
    {
        if (Tipo != TipoFormacao.Curso) return;

        if (curso is null) throw new ArgumentNullException(nameof(curso), "O tipo de curso deve ser selecionado.");
        
        Curso = curso;
    }

    private void AtribuirEnsino(Ensino? ensino)
    {
        if (Tipo != TipoFormacao.Ensino) return;

        if (ensino is null) throw new ArgumentNullException(nameof(ensino), "O tipo de ensino deve ser selecionado.");

        Ensino = ensino;
    }

    private void IncluirPeriodo(DateTime? dataInicio, DateTime? dataFim)
    {
        DataInicio = dataInicio;

        DataFim = dataFim;
    }
}
