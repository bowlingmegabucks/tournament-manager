using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

internal sealed class GetTournamentByIdQueryHandler
    : IQueryHandler<GetRegistrationByIdQuery, Models.Tournament?>
{
    private readonly IRepository _repository;

    public GetTournamentByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Models.Tournament?>> HandleAsync(GetRegistrationByIdQuery query, CancellationToken cancellationToken)
    {
        var tournament = await _repository.RetrieveAsync(query.Id, cancellationToken);

        return tournament is null
            ? Error.NotFound("Tournament not found.") 
            : new Models.Tournament(tournament);
    }
}