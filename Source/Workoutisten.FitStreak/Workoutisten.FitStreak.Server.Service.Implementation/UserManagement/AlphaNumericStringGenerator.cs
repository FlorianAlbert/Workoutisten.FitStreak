using Microsoft.Extensions.Configuration;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;

public class AlphaNumericStringGenerator : IAlphaNumericStringGenerator
{
    private Random Random => new();

    private string PossibleChars { get; }

    private int Length { get; }

    public AlphaNumericStringGenerator(IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        if (configuration["AlphaNumericStringGeneration:PossibleChars"] is null)
        {
            throw new ArgumentException("AlphaNumericStringGeneration:PossibleChars value is null.",
                nameof(configuration));
        }

        if (configuration["AlphaNumericStringGeneration:Length"] is null)
        {
            throw new ArgumentException("AlphaNumericStringGeneration:Length value is null.",
                nameof(configuration));
        }

        PossibleChars = configuration["AlphaNumericStringGeneration:PossibleChars"];

        if (int.TryParse(configuration["AlphaNumericStringGeneration:Length"], out var length)) Length = length;
        else throw new ArgumentException("AlphaNumericStringGeneration:Length value does not contain an integer.", nameof(configuration));
    }

    public Task<string> GenerateAlphaNumericString()
    {
        return Task.FromResult(
            new string(
                Enumerable.Repeat(PossibleChars, Length)
                .Select(s => s[Random.Next(s.Length)]).ToArray()
                )
        );
    }
}
