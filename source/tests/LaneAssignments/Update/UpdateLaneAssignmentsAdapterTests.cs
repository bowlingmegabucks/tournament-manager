using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.LaneAssignments.Update;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.LaneAssignments.Update.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.LaneAssignments.Update.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.LaneAssignments.Update.IBusinessLogic>();

        _adapter = new NortheastMegabuck.LaneAssignments.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        _adapter.Execute(squadId, bowlerId, position);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(squadId, bowlerId, position), Times.Once);
    }

    [Test]
    public void Execute_ErrorSetToBusinessLogicError()
    {
        var errorDetail = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(errorDetail);

        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        _adapter.Execute(squadId, bowlerId, position);

        Assert.That(_adapter.Error, Is.EqualTo(errorDetail));
    }
}
