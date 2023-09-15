using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.ViewModels;

public class EnderecoViewModel
{
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cep { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? MoraCom { get; set; }
    public Domicilio? Domicilio { get; set; }
    public TipoDomicilio? TipoDomicilio { get; set; }
}
