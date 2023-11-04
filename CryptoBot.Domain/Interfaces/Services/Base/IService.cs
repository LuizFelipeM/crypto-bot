namespace CryptoBot.Domain;

public interface IService<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    TEntity? Find(TId id);
    IQueryable<TEntity> GetAll();
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Upsert(TEntity entity);
    void Remove(TEntity entity);
}
