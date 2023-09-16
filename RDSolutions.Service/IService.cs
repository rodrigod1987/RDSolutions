using RDSolutions.Repository.Model.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RDSolutions.Service
{
    public interface IService<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
        IList<TEntity> FindAll();
        Task<IList<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);
        TEntity Find(TKey key);
        Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default);
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Remove(TKey key);
        Task RemoveAsync(TKey key, CancellationToken cancellationToken = default);
    }
}