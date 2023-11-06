using System.Text;

namespace CryptoBot.Domain;

public class JwtConfig
{
    public required string Secret { get; set; }
    public byte[] SecretBytes => Encoding.ASCII.GetBytes(Secret);
}
