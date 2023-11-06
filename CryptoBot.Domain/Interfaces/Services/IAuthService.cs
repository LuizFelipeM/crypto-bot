namespace CryptoBot.Domain;

public interface IAuthService
{
    string Authenticate(string userName, string password);
    string HashPassword(string password);
}
