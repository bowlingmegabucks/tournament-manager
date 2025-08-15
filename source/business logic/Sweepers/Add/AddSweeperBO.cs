
using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Models;
using BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

namespace BowlingMegabucks.TournamentManager.Sweepers.Add;

/// <summary>
/// 
/// </summary>
internal class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly IQueryHandler<GetRegistrationByIdQuery, Models.Tournament?> _getTournamentBO;

    private readonly Lazy<FluentValidation.IValidator<Models.Sweeper>> _validator;
    private FluentValidation.IValidator<Models.Sweeper> Validator => _validator.Value;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    public BusinessLogic(IQueryHandler<GetRegistrationByIdQuery, Models.Tournament?> getTournamentBO, FluentValidation.IValidator<Models.Sweeper> validator, IDataLayer dataLayer)
    {
        _getTournamentBO = getTournamentBO;
        _validator = new Lazy<FluentValidation.IValidator<Models.Sweeper>>(() => validator);
        _dataLayer = new Lazy<IDataLayer>(() => dataLayer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sweeper"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SquadId?> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(sweeper);

        var tournamentResult = await _getTournamentBO.HandleAsync(new() { Id = sweeper.TournamentId }, cancellationToken).ConfigureAwait(false);

        if (tournamentResult.IsError)
        {
            Errors = tournamentResult.Errors.ToErrorDetails();

            return null;
        }

        sweeper.Tournament = tournamentResult.Value;

        var validationResults = await Validator.ValidateAsync(sweeper, cancellationToken).ConfigureAwait(false);

        if (!validationResults.IsValid)
        {
            Errors = validationResults.Errors.Select(e => new Models.ErrorDetail(e.ErrorMessage));

            return null;
        }

        try
        {
            return await DataLayer.ExecuteAsync(sweeper, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Errors = [new Models.ErrorDetail(ex)];

            return null;
        }
    }
}

/// <summary>
/// 
/// </summary>
public interface IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    IEnumerable<Models.ErrorDetail> Errors { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sweeper"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SquadId?> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken);
}
