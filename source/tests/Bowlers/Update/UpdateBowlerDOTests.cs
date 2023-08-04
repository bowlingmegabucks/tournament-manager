namespace NortheastMegabuck.Tests.Bowlers.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Bowlers.IRepository> _repository;

    private NortheastMegabuck.Bowlers.Update.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Bowlers.IRepository>();

        _dataLayer = new NortheastMegabuck.Bowlers.Update.DataLayer(_repository.Object);
    }

    [Test]
    public async Task Execute_BowlerName_RepositoryUpdate_CalledCorrectly()
    {
        var id = BowlerId.New();

        var name = new NortheastMegabuck.Models.PersonName
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
