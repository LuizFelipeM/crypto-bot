
namespace CryptoBot.Domain.Interfaces.Services.Base;

public abstract class Service<TEntity, TId> : IService<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    private readonly IRepository<TEntity, TId> _repository;

    public Service(IRepository<TEntity, TId> repository)
    {
        _repository = repository;
    }

    public TEntity? Find(TId id) => _repository.Find(id);
    public IQueryable<TEntity> GetAll() => _repository.GetAll();
    public void Insert(TEntity entity) => _repository.Insert(entity);
    public void Update(TEntity entity) => _repository.Update(entity);
    public void Upsert(TEntity entity) => _repository.Upsert(entity);
    public void Remove(TEntity entity) => _repository.Remove(entity);
}
