using Core.DomainObjects;

namespace Vagas.Business.Models;

public class Vaga : Entity, IAggregateRoot
{
    public Vaga(string nome, string descricao, Guid clienteId)
    {
        Nome = nome;
        Descricao = descricao;
        ClienteId = clienteId;
    }

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Guid ClienteId { get; set; }

    public void Atualizar(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }
}
