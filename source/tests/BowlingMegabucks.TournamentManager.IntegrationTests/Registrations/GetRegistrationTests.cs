using System.Net;
using System.Net.Http.Json;
using Bogus;
using BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;
using BowlingMegabucks.TournamentManager.IntegrationTests.Bowlers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;
using BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

public sealed class GetRegistrationTests
    : IntegrationTestFixture
{
    public GetRegistrationTests(TournamentManagerWebAppFactory apiFactory)
        : base(apiFactory)
    { }

    [Fact]
    public async Task GetRegistration_ShouldReturn404_WhenNoRegistrationIsFound()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournaments = TournamentEntityFactory.Bogus(3).ToList();
        var bowlers = BowlerEntityFactory.Bogus(5).ToList();
        var divisions = DivisionEntityFactory.Bogus(2, tournaments[0].Id);
        var squads = SquadEntityFactory.Bogus(3, tournaments[0].Id);

        var faker = new Faker();
        var registration1 = RegistrationEntityFactory.Bogus(bowlers[0], faker.PickRandom(divisions), faker.PickRandom(squads, 2));
        var registration2 = RegistrationEntityFactory.Bogus(bowlers[1], faker.PickRandom(divisions), faker.PickRandom(squads, 2));
        var registrationSeeds = new[] { registration1, registration2 };

        await _dbContext.Tournaments.AddRangeAsync(tournaments, TestContext.Current.CancellationToken);
        await _dbContext.Bowlers.AddRangeAsync(bowlers, TestContext.Current.CancellationToken);
        await _dbContext.Divisions.AddRangeAsync(divisions, TestContext.Current.CancellationToken);
        await _dbContext.Squads.AddRangeAsync(squads, TestContext.Current.CancellationToken);

        await _dbContext.Registrations.AddRangeAsync(registrationSeeds, TestContext.Current.CancellationToken);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        using var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/registrations/{RegistrationId.New()}");

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetRegistration_ShouldReturnRegistration_WhenRegistrationExists()
    {
        // Arrange
        await ResetDatabaseAsync();
        
        var tournaments = TournamentEntityFactory.Bogus(3).ToList();
        var bowlers = BowlerEntityFactory.Bogus(5).ToList();
        var divisions = DivisionEntityFactory.Bogus(2, tournaments[0].Id);
        var squads = SquadEntityFactory.Bogus(3, tournaments[0].Id);

        var faker = new Faker();
        var registration1 = RegistrationEntityFactory.Bogus(bowlers[0], faker.PickRandom(divisions), faker.PickRandom(squads, 2));
        var registration2 = RegistrationEntityFactory.Bogus(bowlers[1], faker.PickRandom(divisions), faker.PickRandom(squads, 2));
        var registrationSeeds = new[] { registration1, registration2 };

        var registrationSeed = RegistrationEntityFactory.Bogus(bowlers[2], faker.PickRandom(divisions), faker.PickRandom(squads, 2));

        await _dbContext.Tournaments.AddRangeAsync(tournaments, TestContext.Current.CancellationToken);
        await _dbContext.Bowlers.AddRangeAsync(bowlers, TestContext.Current.CancellationToken);
        await _dbContext.Divisions.AddRangeAsync(divisions, TestContext.Current.CancellationToken);
        await _dbContext.Squads.AddRangeAsync(squads, TestContext.Current.CancellationToken);
        await _dbContext.Registrations.AddAsync(registrationSeed, TestContext.Current.CancellationToken);
        await _dbContext.Registrations.AddRangeAsync(registrationSeeds, TestContext.Current.CancellationToken);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        using var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/registrations/{registrationSeed.Id}");

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var registrationResponse = await response.Content.ReadFromJsonAsync<GetRegistrationResponse>(TestContext.Current.CancellationToken);
        registrationResponse!.Registration.Should().NotBeNull();

        var registration = registrationResponse.Registration;

        registration.Should().NotBeNull();
        registration.Id.Should().Be(registrationSeed.Id);
        
        registration.Division.Id.Should().Be(registrationSeed.DivisionId);
        registration.Squads.Should().HaveCount(registrationSeed.Squads.Count);
    }
}