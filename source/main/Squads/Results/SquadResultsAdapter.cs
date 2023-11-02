
namespace NortheastMegabuck.Squads.Results;
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
        _businessLogic = mockBusinessLogic;
    }

    public async Task<IEnumerable<IGrouping<string, IViewModel>>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var squadResultsByDivision = (await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false)).ToList();

        if (squadResultsByDivision.Count == 0)
        {
            return Enumerable.Empty<IGrouping<string, ViewModel>>();
        }

        var results = new List<ViewModel>();

        foreach (var squadResultInDivision in squadResultsByDivision)
        {
            short place = 1;
            var divisionResult = squadResultInDivision.Single();

            foreach (var advancingScore in divisionResult.AdvancingScores)
            {
                var result = new ViewModel(advancingScore, divisionResult.Squad.Date, place++, true, false);

                results.Add(result);
            }

            foreach (var cashingScore in divisionResult.CashingScores)
            {
                var result = new ViewModel(cashingScore, divisionResult.Squad.Date, place++, false, true);

                results.Add(result);
            }

            foreach (var nonQualifyingScore in divisionResult.NonQualifyingScores)
            {
                var result = new ViewModel(nonQualifyingScore, divisionResult.Squad.Date, place++, false, false);

                results.Add(result);
            }
        }

        return results.GroupBy(result=> result.DivisionName);
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IGrouping<string, IViewModel>>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}