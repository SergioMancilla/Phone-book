using backend.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace backend.Utils;

public class PasswordHashService
{
    public bool VerifyPassword(string hashedPassword, string password)
    {
        var hashed = this.HashPassword(password);
        return hashed.Equals(hashedPassword);
    }

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8)
        );

        return hashed;
    }
}
