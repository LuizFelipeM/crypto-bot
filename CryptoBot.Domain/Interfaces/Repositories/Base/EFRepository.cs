using Microsoft.EntityFrameworkCore;

namespace CryptoBot.Domain.Interfaces.Repositories.Base;

public abstract class EFRepository<TEntity, TId, TDbContext> : IRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;
    private readonly Func<TDbContext, DbSet<TEntity>> _dbSetSelector;
    private readonly Func<TEntity, TId> _idSelector;

    public EFRepository(
        TDbContext dbContext,
        Func<TDbContext, DbSet<TEntity>> dbSetSelector,
        Func<TEntity, TId> idSelector)
    {
        _dbContext = dbContext;
        _dbSetSelector = dbSetSelector;
        _idSelector = idSelector;
    }

    public TEntity? Find(TId id) =>
        _dbSetSelector(_dbContext).Find(id);

    public IQueryable<TEntity> GetAll() =>
        _dbSetSelector(_dbContext).AsQueryable();

    public void Insert(TEntity entity)
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        _dbSetSelector(_dbContext).Add(entity);
        _dbContext.SaveChanges();
        transaction.Commit();
    }

    public void Update(TEntity entity)
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        _dbSetSelector(_dbContext).Update(entity);
        _dbContext.SaveChanges();
        transaction.Commit();
    }

    public void Upsert(TEntity entity)
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        var state = EqualityComparer<TId>.Default.Equals(_idSelector(entity), default(TId)) ? EntityState.Added : EntityState.Modified;
        _dbSetSelector(_dbContext).Entry(entity).State = state;
        _dbContext.SaveChanges();
        transaction.Commit();
    }

    public void Remove(TEntity entity)
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        _dbSetSelector(_dbContext).Remove(entity);
        _dbContext.SaveChanges();
        transaction.Commit();
    }
}
