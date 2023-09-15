using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.Request.Candidato;

public abstract class FormacaoRequest
{
    public string NomeInstituicao { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public TipoFormacao Tipo { get; set; }
    public Ensino? Ensino { get; set; }
    public Curso? Curso { get; set; }
    public StatusFormacao Status { get; set; }
    public PeriodoFormacao Periodo { get; set; }
}
