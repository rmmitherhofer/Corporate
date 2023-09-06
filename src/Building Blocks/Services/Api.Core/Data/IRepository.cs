using Core.DomainObjects;

namespace Api.Core;

public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<IEnumerable<TEntity>> ObterTodos();
    Task<TEntity?> ObterPorId(Guid id);
    void Adicionar(TEntity entity);
    void Atualizar(TEntity entity);
    void Remover(TEntity entity);
}
