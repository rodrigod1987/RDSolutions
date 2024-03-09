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

    Task<IQueryable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default
        , params string[] navigationProperties);

    TEntity Find(TKey key);
        
    Task<TEntity> FindAsync(TKey key
        , CancellationToken cancellationToken = default
        , params string[] navigationProperties);

    TEntity Insert(TEntity entity
        , params string[] navigationProperties);

    Task<TEntity> InsertAsync(TEntity entity
        , CancellationToken cancellationToken = default
        , params string[] navigationProperties);

    TEntity Update(TEntity entity
        , params string[] navigationProperties);

    Task<TEntity> UpdateAsync(TEntity entity
        , CancellationToken cancellationToken = default
        , params string[] navigationProperties);

    void Remove(TEntity entity
        , params string[] navigationProperties);

    Task RemoveAsync(TEntity entity
        , CancellationToken cancellationToken = default
        , params string[] navigationProperties);
}
