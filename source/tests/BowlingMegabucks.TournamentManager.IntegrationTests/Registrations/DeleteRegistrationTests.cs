using BowlingMegabucks.TournamentManager.Api.Registrations.DeleteRegistration;
using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.IntegrationTests.Bowlers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;
using BowlingMegabucks.TournamentManager.IntegrationTests.Sweepers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

public sealed class DeleteRegistrationTests
    : IntegrationTestFixture
{
    public DeleteRegistrationTests(TournamentManagerWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task ShouldReturnError_WhenRegistrationIsNotFound()
    {
        // Arrange
        await ResetDatabaseAsync();

        var registrationId = await CreateTestRegistrationAsync();

        using var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/v1/registrations/{RegistrationId.New()}");

        // Act
        var httpResponse = await CreateAuthenticatedClient().SendAsync(httpRequest, TestContext.Current.CancellationToken);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        _dbContext.Registrations.AsNoTracking().Where(registration => registration.Id == registrationId).Should().ContainSingle();
    }

    [Fact]
    public async Task ShouldReturnError_WhenThereIsAnInternalError()
    {
        // Arrange
        await ResetDatabaseAsync();

        var registrationId = await CreateTestRegistrationAsync(withScores: true);

        using var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/v1/registrations/{registrationId}");

        // Act
        var httpResponse = await CreateAuthenticatedClient().SendAsync(httpRequest, TestContext.Current.CancellationToken);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        _dbContext.Registrations.AsNoTracking().Where(registration => registration.Id == registrationId).Should().ContainSingle();
    }

    [Fact]
    public async Task ShouldDeleteRegistration_WhenRegistrationExists()
    {
        // Arrange
        await ResetDatabaseAsync();

        var registrationId = await CreateTestRegistrationAsync();

        using var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/v1/registrations/{registrationId}");

        // Act
        var httpResponse = await CreateAuthenticatedClient().SendAsync(httpRequest, TestContext.Current.CancellationToken);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        _dbContext.Registrations.AsNoTracking().Where(registration => registration.Id == registrationId).Should().BeEmpty();

        var paymentCount = await _dbContext.Database.SqlQueryRaw<Guid>($"SELECT p.Id FROM Payments p").CountAsync(TestContext.Current.CancellationToken);
        paymentCount.Should().Be(0);
    }

    private async Task<RegistrationId> CreateTestRegistrationAsync(bool withScores = false)
    {
        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(6, tournament.Id);
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);
        var bowler = BowlerEntityFactory.Bogus();

        var registration = RegistrationEntityFactory.Bogus(bowler, division, squads);

        var payment = PaymentEntityFactory.Bogus(1, registration.Id).Single();
        registration.Payments.Add(payment);

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Registrations.Add(registration);

        if (withScores)
        {
            var squadScore = new SquadScore
            {
                SquadId = registration.Squads.First().SquadId,
                BowlerId = registration.BowlerId,
                Game = 1,
                Score = 200
            };
        
            _dbContext.SquadScores.Add(squadScore);
        }

        await _dbContext.SaveChangesAsync();

        return registration.Id;
    }
}