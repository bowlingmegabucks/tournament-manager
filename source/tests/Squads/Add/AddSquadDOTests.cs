using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Squads.Add;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Squads.IEntityMapper> _mapper;
    private Mock<NewEnglandClassic.Squads.IRepository> _repository;

    private NewEnglandClassic.Squads.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NewEnglandClassic.Squads.IEntityMapper>();
        _repository = new Mock<NewEnglandClassic.Squads.IRepository>();

        _dataLayer = new NewEnglandClassic.Squads.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var squad = new NewEnglandClassic.Models.Squad();
        _dataLayer.Execute(squad);

        _mapper.Verify(mapper => mapper.Execute(squad), Times.Once);
    }

    [Test]
    public void Execute_RepositoryExecute_CalledCorrectly()
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NewEnglandClassic.Models.Squad>())).Returns(entity);

        var model = new NewEnglandClassic.Models.Squad();
        _dataLayer.Execute(model);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryAddResponse()
    {
        var guid = Guid.NewGuid();
        _repository.Setup(repository => repository.Add(It.IsAny<NewEnglandClassic.Database.Entities.TournamentSquad>())).Returns(guid);

        var model = new NewEnglandClassic.Models.Squad();
        var actual = _dataLayer.Execute(model);

        Assert.That(actual, Is.EqualTo(guid));
    }
}
