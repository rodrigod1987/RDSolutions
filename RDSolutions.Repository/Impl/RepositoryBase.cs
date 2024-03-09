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
        var dbEntity = DatabaseContext.Set<TEntity>();
        IncludeNavigationProperties(dbEntity, navigationProperties);

        DatabaseContext.Entry(entity).State = EntityState.Added;
        DatabaseContext.SaveChanges();

        return entity;
    }

    public TEntity Update(TEntity entity, params string[] navigationProperties)
    {
        var dbEntity = DatabaseContext.Set<TEntity>();
        IncludeNavigationProperties(dbEntity, navigationProperties);

        DatabaseContext.Entry(entity).State = EntityState.Modified;
        DatabaseContext.SaveChanges();

        return entity;
    }

    public void Remove(TEntity entity, params string[] navigationProperties)
    {
        var dbEntity = DatabaseContext.Set<TEntity>();
        IncludeNavigationProperties(dbEntity, navigationProperties);

        DatabaseContext.Entry(entity).State = EntityState.Deleted;
        DatabaseContext.SaveChanges();
    }

    public async Task<IQueryable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        ThrowIfCancellationRequested(cancellationToken);

        var dbEntity = DatabaseContext.Set<TEntity>();

        IncludeNavigationProperties(dbEntity, navigationProperties);

        return await Task.FromResult(dbEntity
            .AsNoTracking()
            .AsQueryable());
    }

    public async Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        ThrowIfCancellationRequested(cancellationToken);

        if (key == null)
            return null;

        var entity = await DatabaseContext.FindAsync<TEntity>(key);

        if (entity == null)
            return null;

        var dbEntity = DatabaseContext.Set<TEntity>();
        IncludeNavigationProperties(dbEntity, navigationProperties);
        dbEntity.AsNoTracking();

        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        var dbEntity = DatabaseContext.Set<TEntity>();

        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(dbEntity, navigationProperties);

        DatabaseContext.Entry(entity).State = EntityState.Added;
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        var dbEntity = DatabaseContext.Set<TEntity>();

        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(dbEntity, navigationProperties);

        DatabaseContext.Entry(entity).State = EntityState.Modified;
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        var dbEntity = DatabaseContext.Set<TEntity>();
        
        ThrowIfCancellationRequested(cancellationToken);
        IncludeNavigationProperties(dbEntity, navigationProperties);

        DatabaseContext.Entry(entity).State = EntityState.Deleted;
        await DatabaseContext.SaveChangesAsync(cancellationToken);
    }

    private static void ThrowIfCancellationRequested(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            throw new TaskCanceledException();
        }
    }

    private static void IncludeNavigationProperties(DbSet<TEntity> entity, string[] navigationProperties)
    {
        if (navigationProperties != null)
        {
            foreach (var navigation in navigationProperties)
            {
                entity.Include(navigation);
            }
        }
    }
}
