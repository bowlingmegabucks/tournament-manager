using FluentValidation;

namespace NortheastMegabuck.Scores.Update;
internal class BusinessLogic : IBusinessLogic
{
    private readonly List<Models.ErrorDetail> _errors;
    public IEnumerable<Models.ErrorDetail> Errors => _errors;

    private readonly IValidator<IEnumerable<Models.SquadScore>> _validator;

    private readonly Lazy<IDataLayer> _dataLayer;
    private IDataLayer DataLayer => _dataLayer.Value;

    public BusinessLogic(IConfiguration config)
    {
        _errors = new List<Models.ErrorDetail>();
        _validator = new Validator();
        _dataLayer = new Lazy<IDataLayer>(() => new DataLayer(config));
    }

    internal BusinessLogic(IValidator<IEnumerable<Models.SquadScore>> mockValidator, IDataLayer mockDataLayer)
    {
        _errors = new List<Models.ErrorDetail>();
        _validator = mockValidator;
        _dataLayer = new Lazy<IDataLayer>(()=> mockDataLayer);
    }

    public IEnumerable<Models.SquadScore> Execute(IEnumerable<Models.SquadScore> squadScores)
    {
        var valid = new List<Models.SquadScore>();
        var invalid = new List<Models.SquadScore>();

        var bowlerScores = squadScores.GroupBy(squadScore => squadScore.Bowler.Id);

        foreach (var bowlerScore in bowlerScores)
        {
            var result = _validator.Validate(bowlerScore);

            if (result.IsValid)
            {
                valid.AddRange(bowlerScore);
            }
            else
            {
                invalid.AddRange(bowlerScore);
            }
        }

        try
        {
            DataLayer.Execute(valid);

            return invalid;
        }
        catch (Exception ex)
        {
            _errors.Add(new Models.ErrorDetail(ex.Message));

            return squadScores;
        }
    }
}

internal interface IBusinessLogic
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadScores"></param>
    /// <returns>Any scores that have an error</returns>
    IEnumerable<Models.SquadScore> Execute(IEnumerable<Models.SquadScore> squadScores);
}