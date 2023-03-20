namespace NortheastMegabuck.Tests.Bowlers.Update;

[TestFixture]
internal class DataLayer
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
    public void Execute_BowlerName_RepositoryUpdate_CalledCorrectly()
    {
        var id = BowlerId.New();
        var firstName = "firstName";
        var middleInitial = "middleInitial";
        var lastName = "lastName";
        var suffix = "suffix";

        _dataLayer.Execute(id, firstName, middleInitial, lastName, suffix);

        _repository.Verify(repository => repository.Update(id, firstName, middleInitial, lastName, suffix), Times.Once);
    }
}
