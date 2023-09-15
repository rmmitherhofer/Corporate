namespace PanPan.WebApp.Models.ViewModels.Clientes;

public class ClienteViewModel
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; }
    public string Nome { get; set; }
    public string Representante { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? Email { get; set; }

    public EnderecoViewModel Endereco { get; set; }

    public ClienteViewModel()
    {
        Endereco = new();
    }
}
