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

        var name = new NortheastMegabuck.Models.PersonName
        {
            First = "firstName",
            MiddleInitial = "middleInitial",
            Last = "lastName",
            Suffix = "suffix"
        };

        _dataLayer.Execute(id, name);

        _repository.Verify(repository => repository.Update(id, name.First, name.MiddleInitial, name.Last, name.Suffix), Times.Once);
    }
}
