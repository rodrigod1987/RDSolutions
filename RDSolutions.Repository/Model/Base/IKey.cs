namespace RDSolutions.Repository.Model.Base;

public interface IKey<TKey>
{
    public TKey Id { get; set; }
}

