#pragma warning disable CA1040 // Avoid empty interfaces
#pragma warning disable S2326 // Consider using 'ICommand<Success>' instead of 'ICommand'

using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;


public interface ICommand
    : ICommand<Success>;

public interface ICommand<TResponse>;
