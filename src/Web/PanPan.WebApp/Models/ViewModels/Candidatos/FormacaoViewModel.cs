using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.ViewModels.Candidatos;

public class FormacaoViewModel
{
    public Guid Id { get; set; }
    public Guid CandidatoId { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string NomeInstituicao { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty;
    public string? DataInicioString { get; set; } = string.Empty;
    public string? DataFimString { get; set; } = string.Empty;
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public TipoFormacao Tipo { get; set; }
    public Ensino? Ensino { get; set; }
    public Curso? Curso { get; set; }
    public StatusFormacao Status { get; set; }

    public PeriodoFormacao Periodo { get; set; }
}
