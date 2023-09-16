using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RDSolutions.Repository
{
    public interface IDatabaseContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        TEntity Find<TEntity>(params object[] key) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] key) where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IModel Model { get; }
        IQueryable<TEntity> QueryableEntity<TEntity>() where TEntity : class;
        decimal TotalRecords<TEntity>() where TEntity : class;
        Task<decimal> TotalRecordsAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;
    }
}