using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

[ExcludeFromCodeCoverage]
public sealed class WorldStateRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly WorldStateRequirementDictionary _sut;

    public WorldStateRequirementDictionaryTests()
    {
        _sut = new WorldStateRequirementDictionary(Factory);
        return;

        WorldStateRequirement Factory(WorldState expectedValue)
        {
            return new WorldStateRequirement(_mode, expectedValue);
        }
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