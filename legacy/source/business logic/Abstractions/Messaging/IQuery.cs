namespace BowlingMegabucks.TournamentManager.Abstractions.Messaging;

/// <summary>
/// Represents a query that returns a response of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<TResponse>;