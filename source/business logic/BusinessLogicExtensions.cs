using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BowlingMegabucks.TournamentManager.Bowlers;
using BowlingMegabucks.TournamentManager.Database;
using BowlingMegabucks.TournamentManager.Divisions;
using BowlingMegabucks.TournamentManager.LaneAssignments;
using BowlingMegabucks.TournamentManager.Registrations;
using BowlingMegabucks.TournamentManager.Scores;
using BowlingMegabucks.TournamentManager.Squads;
using BowlingMegabucks.TournamentManager.Sweepers;
using BowlingMegabucks.TournamentManager.Tournaments;
using System.Text.RegularExpressions;

namespace BowlingMegabucks.TournamentManager;

/// <summary>
/// 
/// </summary>
public static class BusinessLogicExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);

        services.AddDatabase(config)
            .AddBowlersModule()
            .AddDivisionsModule()
            .AddLaneAssignmentModule()
            .AddRegistrationModule()
            .AddScoresModule()
            .AddSquadsModule()
            .AddSweepersModule()
            .AddTournamentsModule();

        return services;
    }
}

internal static class DataNormalizationExtensions
{ 
    public static string NormalizePhoneNumber(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Remove all non-digit characters
        var digitsOnly = Regex.Replace(input, @"\D", "");

        // If it starts with '1' and is 11 digits long, remove the leading '1'
        if (digitsOnly.Length == 11 && digitsOnly.StartsWith('1'))
        {
            digitsOnly = digitsOnly.Substring(1);
        }

        // Return 10-digit number or empty string if invalid
        return digitsOnly.Length == 10 ? digitsOnly : string.Empty;
    }
}