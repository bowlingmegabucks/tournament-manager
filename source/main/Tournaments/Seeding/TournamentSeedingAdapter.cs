
namespace NortheastMegabuck.Tournaments.Seeding;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Models.ErrorDetail? Error
        => _businessLogic.Error;

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
        _businessLogic= mockBusinessLogic;
    }

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
    {
        var results = await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        var seeds = new List<IViewModel>();

        foreach ( var divisionResult in results)
        {
            var seed = 1;

            foreach (var qualifier in divisionResult.Qualifiers)
            {
                seeds.Add(new ViewModel(seed++, true, divisionResult.AtLargeCashers.Contains(qualifier.Bowler.Id), qualifier));
            }

            foreach (var nonQualifier in divisionResult.NonQualifiers)
            {
                seeds.Add(new ViewModel(seed++, false, divisionResult.AtLargeCashers.Contains(nonQualifier.Bowler.Id), nonQualifier));
            }
        }

        return seeds;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken);
}