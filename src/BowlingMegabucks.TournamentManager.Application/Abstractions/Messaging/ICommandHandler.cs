using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

internal interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task<ErrorOr<Success>> Handle(TCommand command, CancellationToken cancellationToken);
}

internal interface ICommandHandler<TCommand, TResponse>
     where TCommand : ICommand<TResponse>
{
    Task<ErrorOr<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
