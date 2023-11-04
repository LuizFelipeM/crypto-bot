using CryptoBot.Domain.Models.Entities;

namespace CryptoBot.Domain;

public interface IKlineRepository : IRepository<KlineEntity, long>
{
}
