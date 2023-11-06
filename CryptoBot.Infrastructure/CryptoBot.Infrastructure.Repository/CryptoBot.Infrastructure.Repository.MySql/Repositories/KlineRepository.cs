using CryptoBot.Domain;
using CryptoBot.Domain.Interfaces.Repositories.Base;
using CryptoBot.Domain.Models.Entities;
using CryptoBot.Infrastructure.Repository.MySql.Contexts;

namespace CryptoBot.Infrastructure.Repository.MySql;

public class KlineRepository : EFRepository<KlineEntity, long, MySqlDbContext>, IKlineRepository
{

    public KlineRepository(MySqlDbContext dbContext) : base(dbContext,
                                                            x => x.Klines,
                                                            k => k.Id)
    {
    }
}
