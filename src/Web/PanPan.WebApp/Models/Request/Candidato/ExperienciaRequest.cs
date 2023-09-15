using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.Request.Candidato;

public abstract class ExperienciaRequest
{
    public string Empresa { get; set; }
    public string Cargo { get; set; }
    public string? Atividade { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public StatusExperiencia Status { get; set; }
    public RegimeContratacao RegimeContratacao { get; set; }
    public string? Observacoes { get; set; }
    public string? MotivoDesligamento { get; set; }
    public string? Habilidade { get; set; }
    public decimal? UltimoSalario { get; set; }
}
