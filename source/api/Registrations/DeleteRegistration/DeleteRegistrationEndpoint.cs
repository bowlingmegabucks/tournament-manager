using BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;
using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.DeleteRegistration;

/// <summary>
/// 
/// </summary>
public sealed class DeleteRegistrationEndpoint
    : Endpoint<DeleteRegistrationRequest, Results<NoContent, ProblemHttpResult>>
{
    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Delete("/registrations/{Id}");

        Description(d => d
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblemDetails(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblemDetails(StatusCodes.Status404NotFound)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
            .WithName("Delete Registration"));

        Summary(s =>
        {
            s.Summary = "Deletes a registration by its ID.";
            s.Description = "This endpoint allows you to delete a registration using its unique identifier.";

            s.ExampleRequest = new DeleteRegistrationRequest
            {
                Id = RegistrationId.New()
            };

            s.ResponseExamples[StatusCodes.Status204NoContent] = new();
            s.ResponseExamples[StatusCodes.Status401Unauthorized] = HttpStatusCodeResponses.SampleUnauthorized401("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status404NotFound] = HttpStatusCodeResponses.SampleNotFound404("/registrations/{Id}");
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleRateLimitExceeded429();
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/registrations/{Id}");

            s.Response(StatusCodes.Status204NoContent, "Successfully deleted the registration.");
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status404NotFound, "Registration not found.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status429TooManyRequests, "Rate limit exceeded.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "An unexpected error occurred.", HttpContentTypes.ProblemJson);
        });
    }

    private readonly Abstractions.Messaging.ICommandHandler<DeleteRegistrationCommand, Deleted> _commandHandler;
    private readonly ILogger<DeleteRegistrationEndpoint> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commandHandler"></param>
    /// <param name="logger"></param>
    public DeleteRegistrationEndpoint(Abstractions.Messaging.ICommandHandler<DeleteRegistrationCommand, Deleted> commandHandler,
                                       ILogger<DeleteRegistrationEndpoint> logger)
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
    public override async Task<Results<NoContent, ProblemHttpResult>> ExecuteAsync(DeleteRegistrationRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        _logger.LogRequest(req);

        var command = new DeleteRegistrationCommand { Id = req.Id };

        _logger.LogCommand(command);

        var result = await _commandHandler.HandleAsync(command, ct);

        return !result.IsError
            ? TypedResults.NoContent()
            : result.FirstError.Type == ErrorType.NotFound
                ? TypedResults.NotFound().ToProblemDetails(HttpContext.TraceIdentifier)
                : result.Errors.ToProblemDetails("An error occurred while deleting the registration.", HttpContext.TraceIdentifier);
    }
}

/// <summary>
/// These should really be debug, but keeping as information for the first year to have good tracking to what is coming in and going to business logic
/// </summary>
internal static partial class DeleteRegistrationEndpointLogMessages
{
    [LoggerMessage(Level = LogLevel.Information, Message = "DeleteRegistrationRequest: {@Request}")]
    public static partial void LogRequest(this ILogger<DeleteRegistrationEndpoint> logger, DeleteRegistrationRequest request);

    [LoggerMessage(Level = LogLevel.Information, Message = "DeleteRegistrationCommand: {@Command}")]
    public static partial void LogCommand(this ILogger<DeleteRegistrationEndpoint> logger, DeleteRegistrationCommand command);
}