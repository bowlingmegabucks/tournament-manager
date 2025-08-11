using System.Net;
using System.Net.Http.Json;
using BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;
using BowlingMegabucks.TournamentManager.IntegrationTests.Bowlers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;
using BowlingMegabucks.TournamentManager.IntegrationTests.Sweepers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;
using BowlingMegabucks.TournamentManager.Registrations;
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

public sealed class CreateRegistrationTests
    : IntegrationTestFixture
{
    public CreateRegistrationTests(TournamentManagerWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task CreateRegistration_ShouldReturnOk_WhenValidDataIsProvided()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id, gender: Models.Gender.Male);
        var squads = SquadEntityFactory.Bogus(6, tournament.Id);
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var bowlerInput = BowlerInputFactory.Create(gender: Models.Gender.Male);
        var registrationInput = new RegistrationInput
        {
            Bowler = bowlerInput,
            TournamentId = tournament.Id,
            DivisionId = division.Id,
            Squads = squads.Take(2).Select(s => s.Id).ToList(),
            Sweepers = sweepers.Select(s => s.Id).ToList(),
            SuperSweeper = false,
            Payment = PaymentInputFactory.Create(),
        };
        var createRegistrationRequest = new CreateRegistrationRequest
        {
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/registrations")
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
    }

    [Fact]
    public async Task CreateRegistration_ShouldUpdateExistingBowler_WhenBowlerWithSameUsbcIdExists()
    {
        // Arrange
        await ResetDatabaseAsync();

        var bowler = BowlerEntityFactory.Bogus();
        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id, gender: bowler.Gender);
        var squads = SquadEntityFactory.Bogus(6, tournament.Id);
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var bowlerInput = BowlerInputFactory.Create(
            "UpdatedFirstName",
            bowler.MiddleInitial,
            "UpdatedLastName",
            bowler.Suffix,
            bowler.StreetAddress,
            bowler.CityAddress,
            bowler.StateAddress,
            bowler.ZipCode,
            bowler.EmailAddress,
            "555-555-5555", // Different phone number to verify update
            bowler.USBCId,
            bowler.DateOfBirth,
            bowler.Gender
        );
        var registrationInput = new RegistrationInput
        {
            Bowler = bowlerInput,
            TournamentId = tournament.Id,
            DivisionId = division.Id,
            Squads = squads.Take(2).Select(s => s.Id).ToList(),
            Sweepers = sweepers.Select(s => s.Id).ToList(),
            SuperSweeper = false,
            Payment = PaymentInputFactory.Create()
        };
        var createRegistrationRequest = new CreateRegistrationRequest
        {
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/registrations")
        {
            Content = JsonContent.Create(createRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var updatedBowler = await _dbContext.Bowlers.AsNoTracking().SingleAsync(b => b.Id == bowler.Id, TestContext.Current.CancellationToken);

        updatedBowler.FirstName.Should().Be("UpdatedFirstName");
        updatedBowler.LastName.Should().Be("UpdatedLastName");
        updatedBowler.PhoneNumber.Should().Be("5555555555"); // Normalized phone number
    }

    [Fact]
    public async Task CreateRegistration_ShouldReturnBadRequest_WhenBowlerIsNotEligibleForDivision()
    {
        // Arrange
        await ResetDatabaseAsync();

        var bowler = BowlerEntityFactory.Bogus();
        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id, gender: bowler.Gender == Models.Gender.Male
            ? Models.Gender.Female
            : Models.Gender.Male);
        var squads = SquadEntityFactory.Bogus(6, tournament.Id);
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var bowlerInput = BowlerInputFactory.Create(
            bowler.FirstName,
            bowler.MiddleInitial,
            bowler.LastName,
            bowler.Suffix,
            bowler.StreetAddress,
            bowler.CityAddress,
            bowler.StateAddress,
            bowler.ZipCode,
            bowler.EmailAddress,
            bowler.PhoneNumber,
            bowler.USBCId,
            bowler.DateOfBirth,
            bowler.Gender
        );
        var registrationInput = new RegistrationInput
        {
            Bowler = bowlerInput,
            TournamentId = tournament.Id,
            DivisionId = division.Id,
            Squads = squads.Take(2).Select(s => s.Id).ToList(),
            Sweepers = sweepers.Select(s => s.Id).ToList(),
            SuperSweeper = false,
            Payment = PaymentInputFactory.Create()
        };
        var createRegistrationRequest = new CreateRegistrationRequest
        {
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/registrations")
        {
            Content = JsonContent.Create(createRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<FastEndpoints.ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be((int)HttpStatusCode.BadRequest);
        problemDetails.Detail.Should().Be("An error occurred while creating the registration.");
    }

    [Fact]
    public async Task CreateRegistration_ShouldReturnUnauthorized_WhenNoApiKeyIsProvided()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id, gender: Models.Gender.Male);
        var squads = SquadEntityFactory.Bogus(6, tournament.Id);
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var bowlerInput = BowlerInputFactory.Create(gender: Models.Gender.Male);
        var registrationInput = new RegistrationInput
        {
            Bowler = bowlerInput,
            TournamentId = tournament.Id,
            DivisionId = division.Id,
            Squads = squads.Take(2).Select(s => s.Id).ToList(),
            Sweepers = sweepers.Select(s => s.Id).ToList(),
            SuperSweeper = false,
            Payment = PaymentInputFactory.Create()
        };
        var createRegistrationRequest = new CreateRegistrationRequest
        {
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/registrations")
        {
            Content = JsonContent.Create(createRegistrationRequest)
        };

        // Act
        var response = await CreateClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}