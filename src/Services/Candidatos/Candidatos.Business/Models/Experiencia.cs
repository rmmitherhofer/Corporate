using Candidatos.Business.Enums;
using Core.DomainObjects;
using System.Text.Json.Serialization;

namespace Candidatos.Business.Models;

public class Experiencia : Entity
{
    public string Empresa { get; private set; }
    public string Cargo { get; private set; }
    public string? Atividade { get; private set; }
    public RegimeContratacao RegimeContratacao { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public StatusExperiencia Status { get; private set; }
    public string? MotivoDesligamento { get; private set; }
    public string? Observacoes { get; private set; }
    public string? Habilidade { get; private set; }
    public decimal? UltimoSalario { get; private set; }

    [JsonIgnore]
    public Candidato Candidato { get; protected set; }
    public Guid CandidatoId { get; private set; }

    public Experiencia(Guid candidatoId,
        string empresa,
        string cargo,
        RegimeContratacao regimeContratacao,
        string? atividade,
        DateTime dataInicio,
        string? observacoes,
        string? habilidade,
        decimal? ultimoSalario,
        DateTime? dataFim,
        string? motivoDesligamento,
        StatusExperiencia status)
    {
        CandidatoId = candidatoId;
        Empresa = empresa.Trim();
        Cargo = cargo.Trim();
        Habilidade = habilidade?.Trim();
        UltimoSalario = ultimoSalario;
        RegimeContratacao = regimeContratacao;
        Atividade = atividade?.Trim();
        IncluirPeriodo(dataInicio, dataFim, status);
        IncluirMotivoDesligamento(motivoDesligamento);
        Observacoes = observacoes?.Trim();
    }
    public void Alterar(string cargo, 
        string? atividade, 
        RegimeContratacao regimeContratacao, 
        DateTime dataInicio, 
        DateTime? dataFim, 
        string? motivoDesligamento, 
        string? observacoes, 
        string? habilidade,
        decimal? ultimoSalario,
        StatusExperiencia status
        )
    {
        Cargo = cargo.Trim();
        Atividade = atividade?.Trim();
        Habilidade = habilidade?.Trim();
        UltimoSalario = ultimoSalario;
        RegimeContratacao = regimeContratacao;
        IncluirPeriodo(dataInicio, dataFim, status);
        IncluirMotivoDesligamento(motivoDesligamento);
        Observacoes = observacoes?.Trim();
    }
    private void IncluirMotivoDesligamento(string? motivoDesligamento)
    {
        MotivoDesligamento = motivoDesligamento?.Trim();
    }
    private void IncluirPeriodo(DateTime dataInicio, DateTime? dataFim, StatusExperiencia status)
    {
        DataInicio = dataInicio;

        DataFim = dataFim;

        if (DataFim.HasValue)
        {
            if (DataInicio.Date > DataFim.Value.Date) throw new ArgumentOutOfRangeException(nameof(DataInicio), "Data de inicio deve ser menor ou igual a data fim");

            if (DataFim.Value.Date > DateTime.Now.Date) throw new ArgumentOutOfRangeException(nameof(DataFim), "A data de desligamento, deve ser menor ou igual a data atual");

            Status = status;

            return;
        }
        Status = StatusExperiencia.Atual;
    }
}
