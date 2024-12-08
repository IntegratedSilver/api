using System;
using System.Security.Cryptography;
using api.Models.DTO;

namespace api.Services;

public class PasswordService
{
    private const int ITERATION_COUNT = 100000;
    private const int SALT_SIZE = 64;
    private const int HASH_SIZE = 256;

    public PasswordDTO HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        var saltBytes = new byte[SALT_SIZE];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetNonZeroBytes(saltBytes);
        }

        var salt = Convert.ToBase64String(saltBytes);
        using var deriveBytes = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            ITERATION_COUNT,
            HashAlgorithmName.SHA512);

        var hash = Convert.ToBase64String(deriveBytes.GetBytes(HASH_SIZE));

        return new PasswordDTO
        {
            Salt = salt,
            Hash = hash
        };
    }

    public bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        if (string.IsNullOrEmpty(password) || 
            string.IsNullOrEmpty(storedHash) || 
            string.IsNullOrEmpty(storedSalt))
            return false;

        try
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            using var deriveBytes = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                ITERATION_COUNT,
                HashAlgorithmName.SHA512);

            var newHash = Convert.ToBase64String(deriveBytes.GetBytes(HASH_SIZE));
            return newHash == storedHash;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}