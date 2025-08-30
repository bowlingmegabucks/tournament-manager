using BowlingMegabucks.TournamentManager.Domain.Abstractions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Domain.Abstractions;

public sealed class EntityTests
{
    private sealed class TestEntity
        : Entity<int>
    {
        public TestEntity(int id, string property1)
            : base(id)
        {
            Property1 = property1;
        }

        public string Property1 { get; }
    }

    [Fact]
    public void Equality_ShouldEqualEachOther_WhenIdsAreTheSame()
    {
        // Arrange
        var entity1 = new TestEntity(1, "sample1");
        var entity2 = new TestEntity(1, "sample2");

        // Act
        bool areEqual = entity1.Equals(entity2);

        // Assert
        areEqual.Should().BeTrue();
    }

    [Fact]
    public void Equality_ShouldNotEqualEachOther_WhenIdsAreDifferent()
    {
        // Arrange
        var entity1 = new TestEntity(1, "sample1");
        var entity2 = new TestEntity(2, "sample1");

        // Act
        bool areEqual = entity1.Equals(entity2);

        // Assert
        areEqual.Should().BeFalse();
    }
}
