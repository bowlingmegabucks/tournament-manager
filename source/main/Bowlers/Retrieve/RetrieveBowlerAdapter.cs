
namespace BowlingMegabucks.TournamentManager.Bowlers.Retrieve;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    public async Task<IViewModel?> ExecuteAsync(BowlerId bowlerId, CancellationToken cancellationToken)
    {
        var bowler = await _businessLogic.ExecuteAsync(bowlerId, cancellationToken).ConfigureAwait(false);

        return bowler is not null ? new ViewModel(bowler) : null;
    }

    public async Task<IViewModel?> ExecuteAsync(RegistrationId registrationId, CancellationToken cancellationToken)
    {
        var bowler = await _businessLogic.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(false);

        return bowler is not null ? new ViewModel(bowler) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IViewModel?> ExecuteAsync(BowlerId bowlerId, CancellationToken cancellationToken);

    Task<IViewModel?> ExecuteAsync(RegistrationId registrationId, CancellationToken cancellationToken);
}