namespace PanPan.WebApp.Models.Response.Cliente;

public class ClienteResponse
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string? Cnpj { get; set; }
    public string? Nome { get; set; }
    public string? Representante { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? Email { get; set; }

    public EnderecoResponse? Endereco { get; set; }
}