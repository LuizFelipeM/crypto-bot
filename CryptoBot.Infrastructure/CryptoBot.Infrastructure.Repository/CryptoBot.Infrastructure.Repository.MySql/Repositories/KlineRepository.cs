using CryptoBot.Domain;
using CryptoBot.Domain.Models.Entities;
using CryptoBot.Infrastructure.Repository.MySql.Contexts;

namespace CryptoBot.Infrastructure.Repository.MySql;

public class KlineRepository : IKlineRepository
{
    private MySqlDbContext _dbContext;

    public KlineRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public KlineEntity? Find(long id)
    {
        return _dbContext.Kline.Find(id);
    }

    public IQueryable<KlineEntity> GetAll()
    {
        return _dbContext.Kline.AsQueryable();
    }

    public void Insert(KlineEntity entity)
    {
        _dbContext.Kline.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(KlineEntity entity)
    {
        _dbContext.Kline.Update(entity);
        _dbContext.SaveChanges();
    }

    public void Remove(KlineEntity entity)
    {
        _dbContext.Kline.Remove(entity);
        _dbContext.SaveChanges();
    }
}
