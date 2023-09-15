using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.ViewModels.Candidatos;

public class CandidatosViewModel : PageViewModel
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

    public IEnumerable<CandidatoViewModel> Candidatos { get; set; } = new List<CandidatoViewModel>();

}
