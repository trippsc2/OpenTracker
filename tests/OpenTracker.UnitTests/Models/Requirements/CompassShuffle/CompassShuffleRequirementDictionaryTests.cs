using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.CompassShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.CompassShuffle;

[ExcludeFromCodeCoverage]
public sealed class CompassShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly CompassShuffleRequirementDictionary _sut;

    public CompassShuffleRequirementDictionaryTests()
    {
        _sut = new CompassShuffleRequirementDictionary(Factory);
        return;

        CompassShuffleRequirement Factory(bool expectedValue)
        {
            return new CompassShuffleRequirement(_mode, expectedValue);
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
        var sut = scope.Resolve<ICompassShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as CompassShuffleRequirementDictionary);
    }
}