using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CryptoBot.Domain;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CryptoBot.Infrastructure.Service.Auth;

public class AuthService : IAuthService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IUserRepository _userRepository;
    private readonly IWebHostEnvironment _environment;

    public AuthService(
        JwtConfig jwtConfig,
        IUserRepository userRepository,
        IWebHostEnvironment environment)
    {
        _jwtConfig = jwtConfig;
        _userRepository = userRepository;
        _environment = environment;
    }

    private string GenerateToken(UserEntity user)
    {
        List<Claim> claims = new() { new(ClaimTypes.Name, user.UserName.ToString()) };
        if (!string.IsNullOrEmpty(user.Role)) claims.Add(new(ClaimTypes.Role, user.Role.ToString()));

        var key = new SymmetricSecurityKey(_jwtConfig.SecretBytes);
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims,
                                         expires: _environment.IsDevelopment() ? null : DateTime.UtcNow.AddDays(1),
                                         signingCredentials: cred);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string Authenticate(string userName, string password)
    {
        var user = _userRepository.Find(userName)
                   ?? throw new Exception("Invalid user");

        if (!Argon2.Verify(user.PasswordHash, password))
            throw new Exception("Invalid password");

        return GenerateToken(user);
    }

    public string HashPassword(string password) => Argon2.Hash(password);
}
