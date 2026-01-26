using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

public class WorldStateRequirementDictionaryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly WorldStateRequirementDictionary _sut;

    public WorldStateRequirementDictionaryTests()
    {
        static IWorldStateRequirement Factory(WorldState expectedValue)
        {
            return Substitute.For<IWorldStateRequirement>();
        }

        _sut = new WorldStateRequirementDictionary(Factory);
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[WorldState.StandardOpen];
        var requirement2 = _sut[WorldState.StandardOpen];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[WorldState.StandardOpen];
        var requirement2 = _sut[WorldState.Inverted];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IWorldStateRequirementDictionary>();
            
        Assert.NotNull(sut as WorldStateRequirementDictionary);
    }
}