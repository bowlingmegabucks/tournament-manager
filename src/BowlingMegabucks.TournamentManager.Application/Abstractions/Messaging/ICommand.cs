#pragma warning disable CA1040 // Avoid empty interfaces
#pragma warning disable S2326 // Consider using 'ICommand<Success>' instead of 'ICommand'

using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

/// <summary>
/// Represents a command that can be sent to the system.
/// </summary>
public interface ICommand
    : ICommand<Success>;

/// <summary>
/// Represents a command that can be sent to the system.
/// </summary>
/// <typeparam name="TResponse">The type of the response expected from the command.</typeparam>
public interface ICommand<TResponse>;
