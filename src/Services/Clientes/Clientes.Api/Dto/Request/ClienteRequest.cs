namespace Clientes.Api.Dto.Request;

public abstract class ClienteRequest
{
    public string Cnpj { get; set; }
    public string Nome { get; set; }
    public string Representante { get; set; }
    public string Telefone { get; set; }
    public string Celular { get; set; }
    public string Email { get; set; }

    public EnderecoRequest Endereco { get; set; }
}
