using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;

internal sealed class GetRegistrationByIdQueryHandler
    : IQueryHandler<GetRegistrationByIdQuery, Models.Registration?>
{
    private readonly IRepository _repository;

    public GetRegistrationByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Models.Registration?>> HandleAsync(GetRegistrationByIdQuery query, CancellationToken cancellationToken)
    {
        var registration = await _repository.RetrieveAsync(query.Id, cancellationToken);

        return registration is null
            ? Error.NotFound("Registration not found.") 
            : new Models.Registration(registration);
    }
}