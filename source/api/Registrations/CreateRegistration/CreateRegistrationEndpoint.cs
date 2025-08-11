using FastEndpoints;
using BowlingMegabucks.TournamentManager.Api.BogusData;
using BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;
using Microsoft.AspNetCore.Http.HttpResults;
using BowlingMegabucks.TournamentManager.Registrations.Create;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// 
/// </summary>
public sealed class CreateRegistrationEndpoint
    : Endpoint<CreateRegistrationRequest, Results<CreatedAtRoute<CreateRegistrationResponse>,
                                                  ProblemHttpResult>>
{
    /// <summary>
    /// 
    /// </summary>
    public override void Configure()
    {
        Post("/registrations");

        Description(d => d
            .Produces<CreateRegistrationResponse>(StatusCodes.Status201Created, HttpContentTypes.Json)
            .ProducesProblemDetails(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblemDetails(StatusCodes.Status429TooManyRequests)
            .ProducesProblemDetails(StatusCodes.Status500InternalServerError)
            .WithName("Create Registration"));

        Summary(s =>
        {
            s.Summary = "Creates a new registration.";
            s.Description = "This endpoint allows you to create a new registration for a bowler.";

            s.ExampleRequest = new BogusCreateRegistrationRequest();

            var sampleId = RegistrationId.New();
            s.ResponseExamples[StatusCodes.Status201Created] = new CreateRegistrationResponse
            {
                RegistrationId = sampleId
            };
            s.ResponseExamples[StatusCodes.Status400BadRequest] = HttpStatusCodeResponses.SampleBadRequest400("/registrations");
            s.ResponseExamples[StatusCodes.Status429TooManyRequests] = HttpStatusCodeResponses.SampleRateLimitExceeded429();
            s.ResponseExamples[StatusCodes.Status500InternalServerError] = HttpStatusCodeResponses.SampleInternalServerError500("/registrations");

            s.Response<CreateRegistrationResponse>(StatusCodes.Status201Created, "Successfully created the registration.");
            s.Response(StatusCodes.Status401Unauthorized, "Unauthorized access.");
            s.Response<ProblemDetails>(StatusCodes.Status400BadRequest, "Invalid request parameters.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status429TooManyRequests, "Rate limit exceeded.", HttpContentTypes.ProblemJson);
            s.Response<ProblemDetails>(StatusCodes.Status500InternalServerError, "An unexpected error occurred.", HttpContentTypes.ProblemJson);
        });
    }

    private readonly Abstractions.Messaging.ICommandHandler<CreateRegistrationCommand, RegistrationId> _commandHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRegistrationEndpoint"/> class.
    /// This constructor is used to inject the command handler for creating registrations.
    /// </summary>
    /// <param name="commandHandler"></param>
    public CreateRegistrationEndpoint(Abstractions.Messaging.ICommandHandler<CreateRegistrationCommand, RegistrationId> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    /// <summary>
    /// Handles the incoming request to create a new registration.
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public override async Task<Results<CreatedAtRoute<CreateRegistrationResponse>, ProblemHttpResult>> ExecuteAsync(CreateRegistrationRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        var command = new CreateRegistrationCommand
        {
            Bowler = req.Registration.Bowler.ToModel(),
            TournamentId = req.Registration.TournamentId,
            DivisionId = req.Registration.DivisionId,
            Average = req.Registration.Average,
            Squads = req.Registration.Squads,
            Sweepers = req.Registration.Sweepers,
            SuperSweeper = req.Registration.SuperSweeper,
            Payment = req.Registration.Payment.ToModel()
        };

        var result = await _commandHandler.HandleAsync(command, ct);

        if (!result.IsError)
        {
            var response = new CreateRegistrationResponse
            {
                RegistrationId = result.Value
            };

            return TypedResults.CreatedAtRoute(response, GetRegistrationEndpoint.EndpointName, new { Id = response.RegistrationId });
        }

        if (result.FirstError.Type == ErrorOr.ErrorType.Conflict)
        {
            // this needs to call update registration instead.  returning problem for now.
            return TypedResults.Problem("Bowler is already registered for this tournament.", statusCode: StatusCodes.Status409Conflict);
        }

        return result.Errors.ToProblemDetails("An error occurred while creating the registration.", HttpContext.TraceIdentifier);
    }
}