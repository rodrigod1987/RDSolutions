using RDSolutions.Repository.Model.Base;
using RDSolutions.Repository.Repository;
using RDSolutions.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JoasCar.Cadastros.Service.Cadastros
{
    public abstract class ServiceBase<TEntity, TKey> : IService<TEntity, TKey> 
        where TEntity : class, IKey<TKey>
    {
        private readonly IRepository<TEntity, TKey> _repository;

        protected ServiceBase(IRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        public TEntity Find(TKey key)
        {
            return _repository.Find(key);
        }

        public IList<TEntity> FindAll()
        {
            return _repository
                .FindAll()
                .ToList();
        }

        public void Remove(TKey key)
        {
            var entity = _repository.Find(key);
            if (entity != null)
                _repository.Remove(entity);
        }

        public TEntity Add(TEntity entity)
        {
            var saved = _repository.Find(entity.Id);
            if (saved == null)
                return _repository.Insert(entity);

            return _repository.Update(entity);
        }

        public async Task<IList<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _repository.FindAllAsync(cancellationToken);
            return entities.ToList();
        }

        public async Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default)
        {
            return await _repository.FindAsync(key, cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var saved = await _repository.FindAsync(entity.Id, cancellationToken);
            if (saved == null)
                return await _repository.InsertAsync(entity, cancellationToken);

            return await _repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task RemoveAsync(TKey key, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(key, cancellationToken);
            if (entity != null)
                await _repository.RemoveAsync(entity, cancellationToken);
        }
    }
}
