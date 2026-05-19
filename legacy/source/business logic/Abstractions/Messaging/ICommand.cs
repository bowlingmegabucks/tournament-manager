using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Abstractions.Messaging;

/// <summary>
/// Represents a command that can be executed and return a response.
/// </summary>
public interface ICommand
    : ICommand<Success>;

/// <summary>
/// Represents a command that can be executed and return a response.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<TResponse>;