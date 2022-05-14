﻿
using System.Security.Cryptography;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;

public class PasswordHashingService : IPasswordHashingService
{
    public Task<string> HashPasswordForStorageAsync(string password)
    {
        byte[] salt;
        RandomNumberGenerator.Create().GetBytes(salt = new byte[16]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        string savedPasswordHash = Convert.ToBase64String(hashBytes);
        return Task.FromResult(savedPasswordHash);
    }

    public Task<bool> VerifyPasswordAsync(string password, string expectedPasswordHash)
    {
        byte[] hashBytes = Convert.FromBase64String(expectedPasswordHash);

        /* Get the salt */
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        /* Compute the hash on the password the user entered */
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] generatedHash = pbkdf2.GetBytes(20);

        /* Compare the results */
        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != generatedHash[i]) return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }
}
