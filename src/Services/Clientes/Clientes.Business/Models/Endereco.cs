namespace Clientes.Business.Models;

public class Endereco
{
    public string? Logradouro { get; private set; }
    public string? Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string? Bairro { get; private set; }
    public string? Cep { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }
    public Guid ClienteId { get; private set; }

    protected Endereco() { }
    public Endereco(string? logradouro, string? numero, string? complemento, string? bairro, string? cep, string? cidade, string? estado)
    {
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
    }

    public void AtribuirCliente(Guid clienteId) => ClienteId = clienteId;
    public void Alterar(Endereco endereco)
    {
        Logradouro = endereco.Logradouro;
        Numero = endereco.Numero;
        Complemento = endereco.Complemento;
        Bairro = endereco.Bairro;
        Cep = endereco.Cep;
        Cidade = endereco.Cidade;
        Estado = endereco.Estado;
    }
}
