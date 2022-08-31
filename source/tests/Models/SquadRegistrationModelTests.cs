
namespace NortheastMegabuck.Tests.Models;
internal class SquadRegistration
{
    private NortheastMegabuck.Models.SquadRegistration _squadRegistration;

    [OneTimeSetUp]
    public void SetUp()
    {
        var entity = new NortheastMegabuck.Database.Entities.SquadRegistration
        {
            SquadId = SquadId.New(),
            RegistrationId = RegistrationId.New(),
            Registration = new NortheastMegabuck.Database.Entities.Registration
            {
                Bowler = new NortheastMegabuck.Database.Entities.Bowler
                {
                    Id = BowlerId.New()
                },
                Division = new NortheastMegabuck.Database.Entities.Division
                {
                    Id = NortheastMegabuck.Divisions.Id.New()
                }
            },
            LaneAssignment = "12C"
        };

        _squadRegistration = new NortheastMegabuck.Models.SquadRegistration(entity);
    }

    [Test]
    public void Constructor_RegistrationIdMapped()
        => Assert.That(_squadRegistration.RegistrationId, Is.Not.EqualTo(RegistrationId.Empty));

    [Test]
    public void Constructor_SquadIdMapped()
        => Assert.That(_squadRegistration.SquadId, Is.Not.EqualTo(SquadId.Empty));

    [Test]
    public void Constructor_BowlerMapped()
        => Assert.That(_squadRegistration.Bowler.Id, Is.Not.EqualTo(BowlerId.Empty));

    [Test]
    public void Constructor_DivisionMapped()
        => Assert.That(_squadRegistration.Division.Id, Is.Not.EqualTo(NortheastMegabuck.Divisions.Id.Empty));

    [Test]
    public void Constructor_LaneAssignmentMapped()
        => Assert.That(_squadRegistration.LaneAssignment, Is.EqualTo("12C"));
}
