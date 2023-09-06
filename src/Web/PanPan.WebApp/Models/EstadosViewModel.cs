using Newtonsoft.Json;

namespace PanPan.WebApp.Models;

public class EstadosViewModel
{
    [JsonProperty("estados")]
    public IEnumerable<EstadoViewModel> Estados { get; set; }
}

public class EstadoViewModel
{
    [JsonProperty("sigla")]
    public string Sigla { get; set; }
    [JsonProperty("nome")]
    public string Nome { get; set; }
    [JsonProperty("cidades")]
    public IEnumerable<string> Cidades { get; set; }
}
