using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements.Item.SmallKey;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Item.SmallKey;

public class SmallKeyRequirementDictionaryTests
{
    private readonly IDungeonDictionary _dungeons = Substitute.For<IDungeonDictionary>();

    private readonly SmallKeyRequirementDictionary _sut;

    public SmallKeyRequirementDictionaryTests()
    {
        _sut = new SmallKeyRequirementDictionary(
            _dungeons, (item, count) => new SmallKeyRequirement(item, count));
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[(DungeonID.HyruleCastle, 1)];
        var requirement2 = _sut[(DungeonID.HyruleCastle, 1)];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[(DungeonID.HyruleCastle, 1)];
        var requirement2 = _sut[(DungeonID.HyruleCastle, 2)];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<ISmallKeyRequirementDictionary>();
            
        Assert.NotNull(sut as SmallKeyRequirementDictionary);
    }
}