#pragma warning disable CA1040 // Avoid empty interfaces
#pragma warning disable S2326 // Consider using 'ICommand<Success>' instead of 'ICommand'

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

/// <summary>
/// Represents a query that can be sent to the system.
/// </summary>
/// <typeparam name="TResponse">The type of the response expected from the query.</typeparam>
public interface IQuery<TResponse>;
