namespace CryptoBot.Domain;

public interface IUserService : IService<UserEntity, int>
{
    void Register(string username, string password);
}
