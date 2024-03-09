using RDSolutions.Repository.Model.Base;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RDSolutions.Repository.Repository;

/// <summary>
/// Generic Repository interface of database entity
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity, TKey> where TEntity : IKey<TKey>
{

    IQueryable<TEntity> FindAll();
    Task<IQueryable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);
    TEntity Find(TKey key);
    Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default);
    TEntity Insert(TEntity entity);
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    TEntity Update(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Remove(TEntity entity);
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

}
