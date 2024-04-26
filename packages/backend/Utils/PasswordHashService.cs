using backend.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace backend.Utils;

public class PasswordHashService
{

    public bool VerifyPassword(string hashedPassword, string password)
    {
        var hashed = HashPassword(password);
        return hashed == hashedPassword;
    }

    public string HashPassword(string password)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);

        return Convert.ToBase64String(hashedPassword);
    }
}
