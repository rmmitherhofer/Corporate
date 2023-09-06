using Core.DomainObjects;

namespace Clientes.Business.Models;

public class Cliente : Entity, IAggregateRoot
{
    public string Cnpj { get; set; }
    public string NomeFantasia { get; set; }
    public string NomeRepresentante { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }

    protected Cliente() { }
    public Cliente(string cnpj, string nomeFantasia, string nomeRepresentante, string telefone, string email)
    {
        Cnpj = cnpj;
        NomeFantasia = nomeFantasia;
        NomeRepresentante = nomeRepresentante;
        Telefone = telefone;
        Email = email;
    }

    public void Atualizar(string nomeFantasia, string representante, string telefone, string email)
    {
        NomeFantasia = nomeFantasia;
        NomeRepresentante = representante;
        Telefone = telefone;
        Email = email;
    }
}
