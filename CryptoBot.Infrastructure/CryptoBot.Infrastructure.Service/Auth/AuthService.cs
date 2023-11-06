using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CryptoBot.Domain;
using System.Data;
using Isopoh.Cryptography.Argon2;

namespace CryptoBot.Infrastructure.Service.Auth;

public class AuthService : IAuthService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IUserRepository _userRepository;

    public AuthService(
        JwtConfig jwtConfig,
        IUserRepository userRepository)
    {
        _jwtConfig = jwtConfig;
        _userRepository = userRepository;
    }

    private string GenerateToken(UserEntity user)
    {
        List<Claim> claims = new() { new(ClaimTypes.Name, user.UserName.ToString()) };
        if (!string.IsNullOrEmpty(user.Role)) claims.Add(new(ClaimTypes.Role, user.Role.ToString()));

        var key = new SymmetricSecurityKey(_jwtConfig.SecretBytes);
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims,
                                         expires: DateTime.UtcNow.AddDays(1),
                                         signingCredentials: cred);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
        // if (!string.IsNullOrEmpty(user.Role)) claims.Add(new(ClaimTypes.Role, user.Role.ToString()));

        // var tokenDescriptor = new SecurityTokenDescriptor
        // {
        //     Subject = new ClaimsIdentity(claims),
        //     Expires = DateTime.UtcNow.AddHours(2),
        //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtConfig.SecretBytes),
        //                                                 SecurityAlgorithms.HmacSha256Signature)
        // };
        // var token = tokenHandler.CreateToken(tokenDescriptor);
        // return tokenHandler.WriteToken(token);
    }

    public string Authenticate(string userName, string password)
    {
        var user = _userRepository.Find(userName)
                   ?? throw new NoNullAllowedException("Usuário inválidos");

        if (!Argon2.Verify(user.PasswordHash, password))
            throw new Exception("Senha inválida");

        return GenerateToken(user);
    }

    public string HashPassword(string password) => Argon2.Hash(password);
}
