namespace RDSolutions.Repository.Model.Base
{
    public abstract class EntityBase<TKey> : IEntity, IKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
