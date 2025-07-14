using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Bowlers.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    internal DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    async Task<Models.Bowler> IDataLayer.ExecuteAsync(BowlerId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));

    async Task<Models.Bowler> IDataLayer.ExecuteAsync(RegistrationId registrationId, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(registrationId, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<Models.Bowler> ExecuteAsync(BowlerId id, CancellationToken cancellationToken);

    Task<Models.Bowler> ExecuteAsync(RegistrationId registrationId, CancellationToken cancellationToken);
}