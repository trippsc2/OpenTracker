using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.MapShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.MapShuffle;

[ExcludeFromCodeCoverage]
public sealed class MapShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly MapShuffleRequirementDictionary _sut;

    public MapShuffleRequirementDictionaryTests()
    {
        _sut = new MapShuffleRequirementDictionary(Factory);
        return;

        MapShuffleRequirement Factory(bool expectedValue)
        {
            return new MapShuffleRequirement(_mode, expectedValue);
        }
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[false];
        var requirement2 = _sut[false];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[false];
        var requirement2 = _sut[true];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IMapShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as MapShuffleRequirementDictionary);
    }
}