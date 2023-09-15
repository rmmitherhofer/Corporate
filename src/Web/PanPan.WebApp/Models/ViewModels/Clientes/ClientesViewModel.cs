namespace PanPan.WebApp.Models.ViewModels.Clientes;

public class ClientesViewModel : PageViewModel
{
    public string? Cnpj { get; set; }
    public string? Nome { get; set; }
    public string? Representante { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? Email { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    public IEnumerable<ClienteViewModel> Clientes { get; set; } = new List<ClienteViewModel>();

}
