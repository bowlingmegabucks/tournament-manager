using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

internal interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task<ErrorOr<Success>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

internal interface ICommandHandler<TCommand, TResponse>
     where TCommand : ICommand<TResponse>
{
    Task<ErrorOr<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
