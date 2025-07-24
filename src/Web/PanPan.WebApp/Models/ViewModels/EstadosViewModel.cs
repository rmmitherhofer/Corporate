using System.Text.Json.Serialization;

namespace PanPan.WebApp.Models.ViewModels;

public class EstadosViewModel
{
    [JsonPropertyName("estados")]
    public IEnumerable<EstadoViewModel> Estados { get; set; }
}

public class EstadoViewModel
{
    [JsonPropertyName("sigla")]
    public string Sigla { get; set; }
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    [JsonPropertyName("cidades")]
    public IEnumerable<string> Cidades { get; set; }
}
