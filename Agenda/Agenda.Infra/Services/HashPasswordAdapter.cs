using System.Security.Cryptography;
using System.Text;
using Agenda.Domain.Services;

namespace Agenda.Infra.Services;

public class HashPasswordAdapter : IHashPassword
{
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
    private const int Iterations = 10_000;
    private const int KeySize = 256 / 8;
    private const int SaltSize = 128 / 8;
    private const char Delimiter = ';';
    
    public string Generate(string password)
    {
        return this.GenerateHash(password);
    }

    public bool Verify(string passwordHash, string inputPassword)
    {
        var elements = passwordHash.Split(Delimiter);
        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithm, KeySize);
        
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }

    private string GenerateHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);
        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }
}