using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.Request.Candidato;

public abstract class CandidatoRequest
{
    public string Nome { get; set; }
    public Sexo Sexo { get; set; }
    public RecrutadoPor RecrutadoPor { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? CelularAlternativo { get; set; }
    public string? Email { get; set; }
    public EnderecoRequest Endereco { get; set; }
}