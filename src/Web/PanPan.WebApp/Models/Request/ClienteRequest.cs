namespace PanPan.WebApp.Models.Request;

public class AddClienteRequest
{
    public string Cnpj { get; set; }
    public string NomeFantasia { get; set; }
    public string NomeRepresentante { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
}
public class UpdateClienteRequest
{
    public string NomeFantasia { get; set; }
    public string NomeRepresentante { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
}