using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Abstractions.Messaging;

/// <summary>
/// Represents a command handler that can handle a command and return a response.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    /// <summary>
    /// Handles the given command and returns a response.
    /// </summary>
    /// <param name="command">The command to be handled.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <remarks>
    /// Implement this method to process the command and return the result. The command handler should encapsulate all business logic required to handle the command.
    /// </remarks>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. The task result contains an <see cref="ErrorOr{TResponse}"/> indicating either a successful response or an error.
    /// </returns>
    Task<ErrorOr<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Represents a command handler that can handle a command and return a response.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Success>
    where TCommand : ICommand<Success>;