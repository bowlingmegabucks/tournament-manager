using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.Tournaments.Retrieve;
internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    private readonly Lazy<IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>>> _queryHandler;
    private IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> QueryHandler
        => _queryHandler.Value;

    public Models.ErrorDetail? Error { get; private set; }

    public Adapter(IBusinessLogic businessLogic, 
        IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>> queryHandler)
    {
        _queryHandler = new Lazy<IQueryHandler<GetTournamentsQuery, IEnumerable<Models.Tournament>>>(() => queryHandler);
        _businessLogic = new Lazy<IBusinessLogic>(() => businessLogic);
    }

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var tournamentsResult = await QueryHandler.HandleAsync(new(), cancellationToken);

        if (tournamentsResult.IsError)
        {
            Error = new(tournamentsResult.FirstError.Description, -1);

            return Array.Empty<IViewModel>();
        }

        return tournamentsResult.Value.Select(tournament => new ViewModel(tournament)).ToList();
    }

    public async Task<IViewModel?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var tournament = await BusinessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        Error = BusinessLogic.ErrorDetail;

        return tournament != null ? new ViewModel(tournament) : null;
    }

    public async Task<IViewModel?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var tournament = await BusinessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        Error = BusinessLogic.ErrorDetail;

        return tournament != null ? new ViewModel(tournament) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(CancellationToken cancellationToken);

    Task<IViewModel?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<IViewModel?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}