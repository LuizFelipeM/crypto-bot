using CryptoBot.Domain.Models.Entities;

namespace CryptoBot.Domain;

public interface IKlineRepository
{
    KlineEntity? Find(long id);
    IQueryable<KlineEntity> GetAll();
    void Insert(KlineEntity entity);
    void Update(KlineEntity entity);
    void Remove(KlineEntity entity);
}
