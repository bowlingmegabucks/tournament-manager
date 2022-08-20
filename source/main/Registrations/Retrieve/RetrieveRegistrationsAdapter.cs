
namespace NewEnglandClassic.Registrations.Retrieve;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IConfiguration config)
    {
        _businessLogic = new BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Contructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = mockBusinessLogic;
    }

    IEnumerable<TournamentRegistrationViewModel> IAdapter.Execute(TournamentId tournamentId)
    {
        var registrations = _businessLogic.Execute(tournamentId);

        Error = _businessLogic.Error;

        return registrations.Select(registration=> new TournamentRegistrationViewModel(registration));
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<TournamentRegistrationViewModel> Execute(TournamentId tournamentId);
}