using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts;

public class EndKeyLayoutTests
{
    private readonly IRequirement _requirement = Substitute.For<IRequirement>();

    private readonly EndKeyLayout _sut;

    public EndKeyLayoutTests()
    {
        _sut = new EndKeyLayout(_requirement);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void CanBeTrue_ShouldReturnTrue_WhenRequirementMetReturnsTrue(bool expected, bool requirementMet)
    {
        _requirement.Met.Returns(requirementMet);
            
        Assert.Equal(expected, _sut.CanBeTrue(
            new List<DungeonItemID>(), new List<DungeonItemID>(),
            Substitute.For<IDungeonState>()));
    }
}