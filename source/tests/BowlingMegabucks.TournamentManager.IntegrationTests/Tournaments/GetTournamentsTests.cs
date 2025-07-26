namespace BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;

public sealed class GetTournamentsTests
{
    private readonly HttpClient _client;
    
    public GetTournamentsTests(ApiTestFixture fixture)
    {
        _client = fixture.CreateClient();
    }
}