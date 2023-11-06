using CryptoBot.Domain;
using CryptoBot.Domain.Interfaces.Repositories.Base;
using CryptoBot.Infrastructure.Repository.MySql.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CryptoBot.Infrastructure.Repository.MySql;

public class UserRepository : EFRepository<UserEntity, int, MySqlDbContext>, IUserRepository
{
    public UserRepository(MySqlDbContext dbContext) : base(dbContext, x => x.Users, u => u.Id)
    {
    }

    public UserEntity? Find(string username) =>
        _dbContext.Users.Where(u => u.UserName == username).FirstOrDefault();
}
