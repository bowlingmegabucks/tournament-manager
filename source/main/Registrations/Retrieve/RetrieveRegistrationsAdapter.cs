
namespace NortheastMegabuck.Registrations.Retrieve;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IConfiguration config)
    {
        _businessLogic = new BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = mockBusinessLogic;
    }

    async Task<IEnumerable<ITournamentRegistrationViewModel>> IAdapter.ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var registrations = await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        Error = _businessLogic.Error;

        return registrations.Select(registration => new TournamentRegistrationViewModel(registration));
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<ITournamentRegistrationViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}