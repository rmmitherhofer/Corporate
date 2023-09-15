using Core.DomainObjects;
using Core.ValueObjects;

namespace Clientes.Business.Models;

public class Cliente : Entity, IAggregateRoot
{
    public string Cnpj { get; private set; }
    public string Nome { get; private set; }
    public string? Representante { get; private set; }
    public string? Telefone { get; private set; }
    public string? Celular { get; private set; }
    public string? Email { get; private set; }

    public Endereco? Endereco { get; private set; }

    protected Cliente() { }
    public Cliente(string cnpj, string nome, string? representante, string? telefone, string? email, string? celular)
    {
        Cnpj = new Cnpj(cnpj).Numero;
        Nome = nome;
        Representante = representante;
        Telefone = telefone;
        Celular = celular;
        Email = new Email(email).Endereco;
    }

    public void Atualizar(string nome, string? representante, string? telefone, string? email, string? celular)
    {
        Nome = nome;
        Representante = representante;
        Telefone = telefone;
        Email = new Email(email).Endereco;
        Celular = celular;
    }

    public void IncluirEndereco(Endereco endereco)
    {
        Endereco = endereco;
        Endereco?.AtribuirCliente(Id);
    }
    public void AlterarEndereco(Endereco endereco)
    {
        if (Endereco is null)
        {
            IncluirEndereco(endereco);
            return;
        }

        Endereco?.Alterar(endereco);
    }
}
