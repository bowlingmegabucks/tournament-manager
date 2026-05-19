using System.Collections;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;
using BowlingMegabucks.TournamentManager.Database.Entities;
using BowlingMegabucks.TournamentManager.IntegrationTests.Bowlers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Divisions;
using BowlingMegabucks.TournamentManager.IntegrationTests.Infrastructure;
using BowlingMegabucks.TournamentManager.IntegrationTests.Squads;
using BowlingMegabucks.TournamentManager.IntegrationTests.Sweepers;
using BowlingMegabucks.TournamentManager.IntegrationTests.Tournaments;
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.IntegrationTests.Registrations;

public sealed class UpdateRegistrationTests
    : IntegrationTestFixture
{
    public UpdateRegistrationTests(TournamentManagerWebAppFactory factory)
        : base(factory)
    { }

    [Fact]
    public async Task UpdateRegistration_ShouldReturn400_WhenRegistrationIdInRouteDoesNotMatchRegistrationIdInBody()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(3, tournament.Id);
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);
        var bowler = BowlerEntityFactory.Bogus();

        var registration = RegistrationEntityFactory.Create(
            bowler: bowler,
            division: division,
            squadIds: squads.Select(s => s.Id),
            sweeperIds: sweepers.Select(s => s.Id));

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Registrations.Add(registration);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var registrationInput = new UpdateRegistrationInput
        {
            RegistrationId = RegistrationId.New(),
            DivisionId = division.Id,
            SuperSweeper = false,
            Average = 200,
        };

        var updateRegistrationRequest = new UpdateRegistrationRequest
        {
            RegistrationId = RegistrationId.New(),
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Patch, $"v1/registrations/{updateRegistrationRequest.RegistrationId}")
        {
            Content = JsonContent.Create(updateRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<FastEndpoints.ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.ShouldNotBeNull();
        problemDetails!.Title.ShouldBe("Bad Request");
        problemDetails.Errors.First().Reason.ShouldBe("Registration ID in the request does not match the ID in the registration details.");
    }

    [Fact]
    public async Task UpdateRegistration_ShouldReturn400_WhenTryingToAddASquadThatHasAlreadyCompleted()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(3, tournament.Id).ToList();
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);
        var bowler = BowlerEntityFactory.Bogus();

        var registration = RegistrationEntityFactory.Create(
            bowler: bowler,
            division: division,
            squadIds: [squads.Select(s => s.Id).First()],
            sweeperIds: sweepers.Select(s => s.Id));

        squads.Last().Complete = true;

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Registrations.Add(registration);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var registrationInput = new UpdateRegistrationInput
        {
            RegistrationId = registration.Id,
            SquadIds = [squads[0].Id, squads[2].Id]
        };

        var updateRegistrationRequest = new UpdateRegistrationRequest
        {
            RegistrationId = registrationInput.RegistrationId,
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Patch, $"v1/registrations/{updateRegistrationRequest.RegistrationId}")
        {
            Content = JsonContent.Create(updateRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.ShouldNotBeNull();
        problemDetails!.Title.ShouldBe("Bad Request");
        problemDetails.Detail.ShouldBe("Error updating registration.");
        problemDetails.Extensions.ShouldContainKey("errors");

        var errorsJson = (JsonElement)problemDetails.Extensions["errors"]!;
        errorsJson.ValueKind.ShouldBe(JsonValueKind.Array);

        var errors = errorsJson.EnumerateArray().ToList();
        errors.ShouldHaveSingleItem();

        var firstError = errors.First();
        firstError.GetProperty("code").GetString().ShouldBe("Registration.InvalidSquadIds");
        firstError.GetProperty("description").GetString().ShouldBe("Cannot add squad(s) that are already complete.");
        firstError.GetProperty("InvalidSquadIds").GetString().ShouldBe(squads[2].Id.ToString());

        var verifyRegistration = await _dbContext.Registrations
            .Include(registration => registration.Squads)
            .AsNoTrackingWithIdentityResolution()
            .SingleAsync(registration => registration.Id == updateRegistrationRequest.RegistrationId, TestContext.Current.CancellationToken);

        verifyRegistration.Squads.Count.ShouldBe(3);
    }

    [Fact]
    public async Task UpdateRegistration_ShouldReturn400_WhenTryingToRemoveASweeperThatBowlerHasBowledIn()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(3, tournament.Id).ToList();
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);
        var bowler = BowlerEntityFactory.Bogus();

        var registration = RegistrationEntityFactory.Create(
            bowler: bowler,
            division: division,
            squadIds: [squads.Select(s => s.Id).First()],
            sweeperIds: sweepers.Select(s => s.Id));

        var squadScore = new SquadScore
        {
            SquadId = squads[0].Id,
            BowlerId = bowler.Id,
            Game = 1,
            Score = 200
        };

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Registrations.Add(registration);
        _dbContext.SquadScores.Add(squadScore);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var registrationInput = new UpdateRegistrationInput
        {
            RegistrationId = registration.Id,
            SquadIds = [squads[1].Id, squads[2].Id]
        };

        var updateRegistrationRequest = new UpdateRegistrationRequest
        {
            RegistrationId = registrationInput.RegistrationId,
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Patch, $"v1/registrations/{updateRegistrationRequest.RegistrationId}")
        {
            Content = JsonContent.Create(updateRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.ShouldNotBeNull();
        problemDetails!.Title.ShouldBe("Bad Request");
        problemDetails.Detail.ShouldBe("Error updating registration.");
        problemDetails.Extensions.ShouldContainKey("errors");

        var errorsJson = (JsonElement)problemDetails.Extensions["errors"]!;
        errorsJson.ValueKind.ShouldBe(JsonValueKind.Array);

        var errors = errorsJson.EnumerateArray().ToList();
        errors.ShouldHaveSingleItem();

        var firstError = errors.First();
        firstError.GetProperty("code").GetString().ShouldBe("Registration.BowlerHasBowled");
        firstError.GetProperty("description").GetString().ShouldBe("Bowler has already bowled in removed squads.");
        firstError.GetProperty("RemovedSquadIds").GetString().ShouldBe(squads[0].Id.ToString());

        var verifyRegistration = await _dbContext.Registrations
            .Include(registration => registration.Squads)
            .AsNoTrackingWithIdentityResolution()
            .SingleAsync(registration => registration.Id == updateRegistrationRequest.RegistrationId, TestContext.Current.CancellationToken);

        verifyRegistration.Squads.Count.ShouldBe(3);
        verifyRegistration.Squads.ShouldContain(squad => squad.SquadId == squads[0].Id);
    }

    [Fact]
    public async Task UpdateRegistration_ShouldReturn400_WhenTryingToEnterSuperSweeperButNotInEverySweeper()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var division = DivisionEntityFactory.Create(tournament.Id);
        var squads = SquadEntityFactory.Bogus(3, tournament.Id).ToList();
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);
        var bowler = BowlerEntityFactory.Bogus();

        var registration = RegistrationEntityFactory.Create(
            bowler: bowler,
            division: division,
            squadIds: [squads.Select(s => s.Id).First()],
            sweeperIds: [sweepers.Select(s => s.Id).First()],
            superSweeper: false);

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.Add(division);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Registrations.Add(registration);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var registrationInput = new UpdateRegistrationInput
        {
            RegistrationId = registration.Id,
            SuperSweeper = true
        };

        var updateRegistrationRequest = new UpdateRegistrationRequest
        {
            RegistrationId = registrationInput.RegistrationId,
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Patch, $"v1/registrations/{updateRegistrationRequest.RegistrationId}")
        {
            Content = JsonContent.Create(updateRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.ShouldNotBeNull();
        problemDetails!.Title.ShouldBe("Bad Request");
        problemDetails.Detail.ShouldBe("Error updating registration.");
        problemDetails.Extensions.ShouldContainKey("errors");

        var errorsJson = (JsonElement)problemDetails.Extensions["errors"]!;
        errorsJson.ValueKind.ShouldBe(JsonValueKind.Array);

        var errors = errorsJson.EnumerateArray().ToList();
        errors.ShouldHaveSingleItem();

        var firstError = errors.First();
        firstError.GetProperty("code").GetString().ShouldBe("Registration.InvalidSuperSweeper");
        firstError.GetProperty("description").GetString().ShouldBe("Cannot set Super Sweeper when not all sweepers are registered.");

        var verifyRegistration = await _dbContext.Registrations
            .Include(registration => registration.Squads)
            .AsNoTrackingWithIdentityResolution()
            .SingleAsync(registration => registration.Id == updateRegistrationRequest.RegistrationId, TestContext.Current.CancellationToken);

        verifyRegistration.SuperSweeper.ShouldBeFalse();
    }

    [Fact]
    public async Task UpdateRegistration_ShouldReturn400_WhenTryingToSwitchDivisionWithDifferentGender()
    {
        // Arrange
        await ResetDatabaseAsync();

        var tournament = TournamentEntityFactory.Bogus();
        var divisionM = DivisionEntityFactory.Create(tournament.Id, gender: Models.Gender.Male);
        var divisionF = DivisionEntityFactory.Create(tournament.Id, gender: Models.Gender.Female);
        var squads = SquadEntityFactory.Bogus(3, tournament.Id).ToList();
        var sweepers = SweeperEntityFactory.Bogus(2, tournament.Id);
        var bowler = BowlerEntityFactory.Bogus();

        divisionM.MaximumAge = null;
        divisionF.MaximumAge = null;
        divisionM.MinimumAge = null;
        divisionF.MinimumAge = null;
        divisionM.HandicapBase = null;
        divisionF.HandicapBase = null;
        divisionM.HandicapPercentage = null;
        divisionF.HandicapPercentage = null;
        divisionM.MaximumHandicapPerGame = null;
        divisionF.MaximumHandicapPerGame = null;

        var registration = RegistrationEntityFactory.Create(
            bowler: bowler,
            division: bowler.Gender == Models.Gender.Male ? divisionM : divisionF,
            squadIds: [squads.Select(s => s.Id).First()],
            sweeperIds: [sweepers.Select(s => s.Id).First()],
            superSweeper: false);

        _dbContext.Tournaments.Add(tournament);
        _dbContext.Divisions.AddRange(divisionM, divisionF);
        _dbContext.Squads.AddRange(squads);
        _dbContext.Sweepers.AddRange(sweepers);
        _dbContext.Bowlers.Add(bowler);
        _dbContext.Registrations.Add(registration);

        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var registrationInput = new UpdateRegistrationInput
        {
            RegistrationId = registration.Id,
            DivisionId = bowler.Gender == Models.Gender.Male ? divisionF.Id : divisionM.Id
        };

        var updateRegistrationRequest = new UpdateRegistrationRequest
        {
            RegistrationId = registrationInput.RegistrationId,
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Patch, $"v1/registrations/{updateRegistrationRequest.RegistrationId}")
        {
            Content = JsonContent.Create(updateRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>(TestContext.Current.CancellationToken);

        problemDetails.ShouldNotBeNull();
        problemDetails!.Title.ShouldBe("Bad Request");
        problemDetails.Detail.ShouldBe("Error updating registration.");
        problemDetails.Extensions.ShouldContainKey("errors");

        var errorsJson = (JsonElement)problemDetails.Extensions["errors"]!;
        errorsJson.ValueKind.ShouldBe(JsonValueKind.Array);

        var errors = errorsJson.EnumerateArray().ToList();
        errors.ShouldHaveSingleItem();

        var firstError = errors.First();
        firstError.GetProperty("description").GetString().ShouldBe("Invalid gender for selected division");

        var verifyRegistration = await _dbContext.Registrations
            .Include(registration => registration.Squads)
            .AsNoTrackingWithIdentityResolution()
            .SingleAsync(registration => registration.Id == updateRegistrationRequest.RegistrationId, TestContext.Current.CancellationToken);

        verifyRegistration.DivisionId.ShouldBe(bowler.Gender == Models.Gender.Male ? divisionM.Id : divisionF.Id);
    }

    [Fact]
    public async Task UpdateRegistration_ShouldReturn204_WhenRegistrationUpdatesSuccessfully()
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
            superSweeper: false);

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

        var registrationInput = new UpdateRegistrationInput
        {
            RegistrationId = registration.Id,
            DivisionId = newDivision.Id,
            SquadIds = [squads[0].Id, squads[2].Id],
            SweeperIds = [sweepers[0].Id, sweepers[1].Id, sweepers[2].Id],
            Average = 200,
            SuperSweeper = true,
            Payment = PaymentInputFactory.Create(confirmationCode: "abc123")
        };

        var updateRegistrationRequest = new UpdateRegistrationRequest
        {
            RegistrationId = registrationInput.RegistrationId,
            Registration = registrationInput
        };

        using var request = new HttpRequestMessage(HttpMethod.Patch, $"v1/registrations/{updateRegistrationRequest.RegistrationId}")
        {
            Content = JsonContent.Create(updateRegistrationRequest)
        };

        // Act
        var response = await CreateAuthenticatedClient().SendAsync(request, TestContext.Current.CancellationToken);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var verifyRegistration = await _dbContext.Registrations
            .Include(registration => registration.Squads)
            .Include(registration => registration.Payments)
            .AsNoTrackingWithIdentityResolution()
            .SingleAsync(registration => registration.Id == updateRegistrationRequest.RegistrationId, TestContext.Current.CancellationToken);

        verifyRegistration.DivisionId.ShouldBe(newDivision.Id);
        verifyRegistration.Squads.Count.ShouldBe(5);
        verifyRegistration.Squads.ShouldContain(squad => squad.SquadId == squads[0].Id);
        verifyRegistration.Squads.ShouldContain(squad => squad.SquadId == squads[2].Id);
        verifyRegistration.Squads.ShouldContain(squad => squad.SquadId == sweepers[0].Id);
        verifyRegistration.Squads.ShouldContain(squad => squad.SquadId == sweepers[1].Id);
        verifyRegistration.Squads.ShouldContain(squad => squad.SquadId == sweepers[2].Id);
        verifyRegistration.Average.ShouldBe(200);
        verifyRegistration.SuperSweeper.ShouldBeTrue();
        verifyRegistration.Payments.Count.ShouldBe(2);
        verifyRegistration.Payments.ShouldContain(payment => payment.ConfirmationCode == "Test_abc123");
        verifyRegistration.Payments.ShouldContain(p => p.Id == payment.Id);
    }
}