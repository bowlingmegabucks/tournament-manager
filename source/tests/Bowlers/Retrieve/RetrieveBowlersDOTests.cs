using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Bowlers.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Bowlers.IRepository> _repository;

    private NortheastMegabuck.Bowlers.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Bowlers.IRepository>();

        _dataLayer = new NortheastMegabuck.Bowlers.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_BowlerId_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<BowlerId>())).Returns(new NortheastMegabuck.Database.Entities.Bowler());

        var bowlerId = BowlerId.New();

        _dataLayer.Execute(bowlerId);

        _repository.Verify(repository=> repository.Retrieve(bowlerId), Times.Once);
    }

    [Test]
    public void Execute_BowlerId_ReturnsRepositoryRetrieve()
    {
        var bowler = new NortheastMegabuck.Database.Entities.Bowler
        {
            LastName = "test"
        };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<BowlerId>())).Returns(bowler);

        var actual = _dataLayer.Execute(BowlerId.New());

        Assert.That(actual.Name.Last, Is.EqualTo("test"));
    }
}
