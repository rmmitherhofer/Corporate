using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.Request.Candidato;

public class GetCandidatoRequest
{
    public int? Codigo { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? CelularAlternativo { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public Sexo? Sexo { get; set; }
    public StatusCandidato? Status { get; set; }
}
