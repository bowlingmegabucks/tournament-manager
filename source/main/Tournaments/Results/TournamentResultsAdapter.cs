
namespace NortheastMegabuck.Tournaments.Results;

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

    public async Task<IEnumerable<IAtLargeViewModel>> AtLargeAsync(TournamentId id, CancellationToken cancellationToken)
    {
        var results = await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        var atLarges = new List<IAtLargeViewModel>();

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
        foreach (var result in results)
        {
            short place = 1;

            foreach (var atLargeScore in result.AtLarge.AdvancingScores)
            {
                var atLarge = new AtLargeViewModel(place++, atLargeScore, result.AtLarge.AdvancersWhoPreviouslyCashed.Contains(atLargeScore.Bowler.Id));
                atLarges.Add(atLarge);
            }
        }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions

        return atLarges;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IAtLargeViewModel>> AtLargeAsync(TournamentId id, CancellationToken cancellationToken);
}