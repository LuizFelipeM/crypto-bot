namespace CryptoBot.Domain;

public interface IUserRepository : IRepository<UserEntity, int>
{
    UserEntity? Find(string username);
}
