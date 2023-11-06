using System.Data;
using CryptoBot.Domain;
using CryptoBot.Domain.Interfaces.Services.Base;

namespace CryptoBot.Infrastructure.Service.User;

public class UserService : Service<UserEntity, int>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public UserService(
        IUserRepository userRepository,
        IAuthService authService) : base(userRepository)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public void Register(string username, string password)
    {
        UserEntity user = new()
        {
            UserName = username,
            PasswordHash = _authService.HashPassword(password),
        };
        _userRepository.Insert(user);
    }
}
