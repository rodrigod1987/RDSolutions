using Microsoft.EntityFrameworkCore;
using RDSolutions.Repository;
using RDSolutions.Repository.Model.Base;
using RDSolutions.Repository.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RDSolutions.Impl.Repository
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : class, IKey<TKey>
    {
        public RepositoryBase(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        protected IDatabaseContext DatabaseContext { get; }

        public IQueryable<TEntity> FindAll()
        {
            return DatabaseContext
                .Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();
        }

        public TEntity Find(TKey key)
        {
            if (key == null)
                return null;

            var entity = DatabaseContext.Find<TEntity>(key);

            if (entity == null)
                return null;

            DatabaseContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            DatabaseContext.Entry(entity).State = EntityState.Added;
            var count = DatabaseContext.SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            DatabaseContext.Entry(entity).State = EntityState.Modified;
            var count = DatabaseContext.SaveChanges();

            return entity;
        }

        public void Remove(TEntity entity)
        {
            DatabaseContext.Set<TEntity>().Remove(entity);
            var count = DatabaseContext.SaveChanges();
        }

        public async Task<IQueryable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfCancellationRequested(cancellationToken);

            return await Task.FromResult(DatabaseContext
                .Set<TEntity>()
                .AsNoTracking()
                .AsQueryable());
        }

        public async Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default)
        {
            ThrowIfCancellationRequested(cancellationToken);

            if (key == null)
                return null;

            var entity = await DatabaseContext.FindAsync<TEntity>(key);

            if (entity == null)
                return null;

            DatabaseContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfCancellationRequested(cancellationToken);

            DatabaseContext.Entry(entity).State = EntityState.Added;
            var count = await DatabaseContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfCancellationRequested(cancellationToken);

            DatabaseContext.Entry(entity).State = EntityState.Modified;
            var count = await DatabaseContext.SaveChangesAsync();

            return entity;
        }

        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfCancellationRequested(cancellationToken);

            DatabaseContext.Set<TEntity>().Remove(entity);
            var count = await DatabaseContext.SaveChangesAsync();
        }

        private void ThrowIfCancellationRequested(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }
        }
    }
}
