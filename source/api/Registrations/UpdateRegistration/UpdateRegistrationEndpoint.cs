using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using BowlingMegabucks.TournamentManager.Api.BogusData;
using BowlingMegabucks.TournamentManager.Registrations.UpdateRegistration;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;

/// <summary>
/// 
/// </summary>
public sealed class UpdateRegistrationEndpoint
    : Endpoint<UpdateRegistrationRequest, Results<NoContent, ProblemHttpResult>>
{
    private const string _route = "/registrations/{RegistrationId}";

    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Patch(_route);

        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblemDetails(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError)
            .WithName("Update Registration"));

        Summary(s =>
        {
            s.Summary = "Updates an existing registration.";
            s.Description = "This endpoint allows you to update an existing registration for a bowler.  This only allows updates to squads/sweepers/super sweeper entered.";

            s.ExampleRequest = new BogusUpdateRegistrationRequest().Generate();

            s.ResponseExamples[StatusCodes.Status400BadRequest] = HttpStatusCodeResponses.SampleBadRequest400(_route);
            s.ResponseExamples[StatusCodes.Status401Unauthorized] = HttpStatusCodeResponses.SampleUnauthorized401(_route);
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleRateLimitExceeded429();
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500(_route);

            s.Response<NoContent>(StatusCodes.Status204NoContent, "Successfully updated the registration.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status429TooManyRequests, "Rate limit exceeded.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "An unexpected error occurred.", HttpContentTypes.ProblemJson);
        });
    }

    private readonly Abstractions.Messaging.ICommandHandler<UpdateRegistrationCommand, Updated> _commandHandler;
    private readonly ILogger<UpdateRegistrationEndpoint> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commandHandler"></param>
    /// <param name="logger"></param>
    public UpdateRegistrationEndpoint(Abstractions.Messaging.ICommandHandler<UpdateRegistrationCommand, Updated> commandHandler,
                                       ILogger<UpdateRegistrationEndpoint> logger)
    {
        _commandHandler = commandHandler;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task<Results<NoContent, ProblemHttpResult>> ExecuteAsync(UpdateRegistrationRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        _logger.LogRequest(req);

        var command = new UpdateRegistrationCommand
        {
            Id = req.RegistrationId,
            DivisionId = req.Registration.DivisionId,
            Average = req.Registration.Average,
            SquadIds = req.Registration.SquadIds,
            SweeperIds = req.Registration.SweeperIds,
            SuperSweeper = req.Registration.SuperSweeper,
            Payment = req.Registration.Payment?.ToModel()
        };

        _logger.LogCommand(command);

        var result = await _commandHandler.HandleAsync(command, ct);

        return !result.IsError
            ? TypedResults.NoContent()
            : result.Errors.ToProblemDetails("Error updating registration.", HttpContext.TraceIdentifier);
    }
}

/// <summary>
/// These should really be debug, but keeping as information for the first year to have good tracking to what is coming in and going to business logic
/// </summary>
internal static partial class UpdateRegistrationEndpointLogMessages
{
    [LoggerMessage(Level = LogLevel.Information, Message = "UpdateRegistrationRequest: {@Request}")]
    public static partial void LogRequest(this ILogger<UpdateRegistrationEndpoint> logger, UpdateRegistrationRequest request);

    [LoggerMessage(Level = LogLevel.Information, Message = "UpdateRegistrationCommand: {@Command}")]
    public static partial void LogCommand(this ILogger<UpdateRegistrationEndpoint> logger, UpdateRegistrationCommand command);
}