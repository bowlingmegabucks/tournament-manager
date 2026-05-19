namespace BowlingMegabucks.TournamentManager.UnitTests.Bowlers.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Bowlers.IRepository> _repository;
    private Mock<TournamentManager.Bowlers.IEntityMapper> _entityMapper;

    private TournamentManager.Bowlers.Update.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<TournamentManager.Bowlers.IRepository>();
        _entityMapper = new Mock<TournamentManager.Bowlers.IEntityMapper>();

        _dataLayer = new TournamentManager.Bowlers.Update.DataLayer(_repository.Object, _entityMapper.Object);
    }

    [Test]
    public async Task Execute_BowlerName_RepositoryUpdate_CalledCorrectly()
    {
        var id = BowlerId.New();

        var name = new TournamentManager.Models.PersonName
        {
            First = "firstName",
            MiddleInitial = "middleInitial",
            Last = "lastName",
            Suffix = "suffix"
        };
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(id, name, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.UpdateAsync(id, name.First, name.MiddleInitial, name.Last, name.Suffix, cancellationToken), Times.Once);
    }
}
