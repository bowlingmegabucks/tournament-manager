using System.Net;
using System.Net.Http.Json;
using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;
using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.IntegrationTests.Bowlers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;
using BowlingMegabucks.TournamentManager.IntegrationTests.Sweepers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;
using BowlingMegabucks.TournamentManager.Registrations;
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

public sealed class AppendRegistrationTests
    : IntegrationTestFixture
{
    public AppendRegistrationTests(TournamentManagerWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task AppendRegistration_ShouldReturn401_WhenNotAuthenticated()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var bowler = BowlerEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(2, tournament.Id).ToList();
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);

        var existingRegistration = RegistrationEntityFactory.Create(
            division: division,
            bowler: bowler,
            squadIds: [squads[0].Id],
            sweeperIds: [],
            superSweeper: false,
            average: 200,
            payment: PaymentEntityFactory.Bogus()
        );

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Registrations.Add(existingRegistration);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var bowlerInput = BowlerInputFactory.Create(usbcId: bowler.USBCId);
        var registrationInput = new RegistrationInput
        {
            Bowler = bowlerInput,
            TournamentId = tournament.Id,
            DivisionId = division.Id,
            Squads = [squads[1].Id],
            Sweepers = [],
            SuperSweeper = false,
            Average = 201
        };

        var createRegistrationRequest = new CreateRegistrationRequest
        {
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "v1/registrations")
        {
            Content = JsonContent.Create(createRegistrationRequest)
        };

        // Act
        var response = await CreateClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AppendRegistration_ShouldReturn400_WhenInvalidDataIsProvided()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var bowler = BowlerEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(2, tournament.Id).ToList();
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);

        var existingRegistration = RegistrationEntityFactory.Create(
            division: division,
            bowler: bowler,
            squadIds: [squads[0].Id],
            sweeperIds: [],
            superSweeper: false,
            average: 200,
            payment: PaymentEntityFactory.Bogus()
        );

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Registrations.Add(existingRegistration);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var bowlerInput = BowlerInputFactory.Create(usbcId: bowler.USBCId);
        var registrationInput = new RegistrationInput
        {
            Bowler = bowlerInput,
            TournamentId = tournament.Id,
            DivisionId = division.Id,
            Squads = [squads[0].Id, squads[1].Id], // already registered for squads[0]
            Sweepers = [],
            SuperSweeper = false,
            Average = 201
        };

        var createRegistrationRequest = new CreateRegistrationRequest
        {
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "v1/registrations")
        {
            Content = JsonContent.Create(createRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var verifyRegistration = await _dbContext.Registrations
            .Include(registration => registration.Squads)
            .Include(registration => registration.Bowler)
            .AsNoTrackingWithIdentityResolution()
            .SingleAsync(registration => registration.Id == existingRegistration.Id, TestContext.Current.CancellationToken);

        verifyRegistration.Squads.Should().ContainSingle();
        verifyRegistration.Bowler.FirstName.Should().Be(bowler.FirstName);
        verifyRegistration.Bowler.LastName.Should().Be(bowler.LastName);
        verifyRegistration.Average.Should().Be(200);
    }

    [Fact]
    public async Task AppendRegistration_ShouldReturn201_WhenDataIsValid()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(3, tournament.Id).ToList();
        var sweepers = SweeperEntityFactory.Bogus(3, tournament.Id).ToList();
        var bowler = BowlerEntityFactory.Bogus();

        var registration = RegistrationEntityFactory.Create(
            bowler: bowler,
            division: division,
            squadIds: squads.Select(s => s.Id).Take(2),
            sweeperIds: sweepers.Select(s => s.Id).Take(2),
            superSweeper: false,
            average: 199);

        var payment = PaymentEntityFactory.Bogus(1, registration.Id).Single();
        registration.Payments.Clear();
        registration.Payments.Add(payment);

        var newDivision = new Division
        {
            Id = DivisionId.New(),
            TournamentId = tournament.Id,
            HandicapPercentage = .9m,
            HandicapBase = 210,
            MaximumHandicapPerGame = 25
        };

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.AddRange(division, newDivision);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Registrations.Add(registration);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var newRegistration = new RegistrationInput
        {
            Bowler = bowler!.ToInput(),
            TournamentId = tournament.Id,
            DivisionId = newDivision.Id,
            Squads = [squads[2].Id],
            Sweepers = [sweepers[2].Id],
            SuperSweeper = true,
            Average = 200
        };

        var createRegistrationRequest = new CreateRegistrationRequest
        {
            Registration = newRegistration
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "v1/registrations")
        {
            Content = JsonContent.Create(createRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var registrationResponse = await response.Content.ReadFromJsonAsync<CreateRegistrationResponse>(TestContext.Current.CancellationToken);
        registrationResponse.Should().NotBeNull();
        registrationResponse!.RegistrationId.Should().NotBe(RegistrationId.Empty);

        response.Headers.Location!.ToString().Should().Be($"http://localhost/v1/registrations/{registrationResponse.RegistrationId}");
        registrationResponse.RegistrationId.Should().Be(registration.Id);

        var verifyRegistration = await _dbContext.Registrations
            .Include(registration => registration.Squads)
            .Include(registration => registration.Bowler)
            .AsNoTrackingWithIdentityResolution()
            .SingleAsync(r => r.Id == registration.Id, TestContext.Current.CancellationToken);

        verifyRegistration.Squads.Should().HaveCount(6);
        verifyRegistration.Bowler.FirstName.Should().Be(bowler.FirstName);
        verifyRegistration.Bowler.LastName.Should().Be(bowler.LastName);
        verifyRegistration.Average.Should().Be(200);
        verifyRegistration.SuperSweeper.Should().BeTrue();
    }
}