using Microsoft.EntityFrameworkCore;
using RDSolutions.Repository;
using RDSolutions.Repository.Model.Base;
using RDSolutions.Repository.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RDSolutions.Impl.Repository;

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

    public TEntity Insert(TEntity entity, params string[] navigationProperties)
    {
        IncludeNavigationProperties(navigationProperties);

        DatabaseContext.Set<TEntity>().Add(entity);
        DatabaseContext.SaveChanges();

        return entity;
    }

    public TEntity Update(TEntity entity, params string[] navigationProperties)
    {
        IncludeNavigationProperties(navigationProperties);

        DatabaseContext.Set<TEntity>().Update(entity);
        DatabaseContext.SaveChanges();

        return entity;
    }

    public void Remove(TEntity entity, params string[] navigationProperties)
    {
        IncludeNavigationProperties(navigationProperties);

        DatabaseContext.Set<TEntity>().Remove(entity);
        DatabaseContext.SaveChanges();
    }

    public async Task<IQueryable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(navigationProperties);

        return await Task.FromResult(DatabaseContext
            .Set<TEntity>()
            .AsNoTracking()
            .AsQueryable());
    }

    public async Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(navigationProperties);

        if (key == null)
            return null;

        var entity = await DatabaseContext.FindAsync<TEntity>(key);

        if (entity == null)
            return null;

        DatabaseContext.Set<TEntity>().AsNoTracking();

        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(navigationProperties);

        DatabaseContext.Set<TEntity>().Add(entity);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(navigationProperties);

        DatabaseContext.Set<TEntity>().Update(entity);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(navigationProperties);

        DatabaseContext.Set<TEntity>().Remove(entity);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private static void ThrowIfCancellationRequested(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            throw new TaskCanceledException();
        }
    }

    private void IncludeNavigationProperties(string[] navigationProperties)
    {
        var dbSet = DatabaseContext.Set<TEntity>();
        if (navigationProperties != null)
        {
            foreach (var navigation in navigationProperties)
            {
                dbSet.Include(navigation);
            }
        }
    }
}
