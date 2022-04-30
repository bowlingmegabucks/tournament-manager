using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Tournaments.Add;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Tournaments.IEntityMapper> _mapper;
    private Mock<NewEnglandClassic.Tournaments.IRepository> _repository;

    private NewEnglandClassic.Tournaments.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NewEnglandClassic.Tournaments.IEntityMapper>();
        _repository = new Mock<NewEnglandClassic.Tournaments.IRepository>();

        _dataLayer = new NewEnglandClassic.Tournaments.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var tournament = new NewEnglandClassic.Models.Tournament();

        _dataLayer.Execute(tournament);
        
        _mapper.Verify(mapper => mapper.Execute(tournament), Times.Once);
    }

    [Test]
    public void Execute_RepositoryAdd_CalledCorrectly()
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NewEnglandClassic.Models.Tournament>())).Returns(entity);
        
        var tournament = new NewEnglandClassic.Models.Tournament();

        _dataLayer.Execute(tournament);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsNewGUID()
    {
        var guid = Guid.NewGuid();
        _repository.Setup(repository => repository.Add(It.IsAny<NewEnglandClassic.Database.Entities.Tournament>())).Returns(guid);

        var tournament = new NewEnglandClassic.Models.Tournament();

        var result = _dataLayer.Execute(tournament);

        Assert.That(result, Is.EqualTo(guid));
    }
}
