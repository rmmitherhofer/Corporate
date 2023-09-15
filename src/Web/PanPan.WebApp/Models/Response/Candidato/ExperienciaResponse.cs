using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.Response.Candidato;

public class ExperienciaResponse
{
    public Guid Id { get; set; }
    public Guid CandidatoId { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string Empresa { get; set; }
    public string Cargo { get; set; }
    public string? Atividade { get; set; }
    public RegimeContratacao RegimeContratacao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public StatusExperiencia Status { get; set; }
    public string? MotivoDesligamento { get; set; }
    public string? Observacoes { get; set; }
    public string? Habilidade { get; set; }
    public decimal? UltimoSalario { get; set; }
}
