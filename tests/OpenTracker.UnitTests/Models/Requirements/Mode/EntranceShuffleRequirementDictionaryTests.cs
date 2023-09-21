using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

[ExcludeFromCodeCoverage]
public sealed class EntranceShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly EntranceShuffleRequirementDictionary _sut;

    public EntranceShuffleRequirementDictionaryTests()
    {
        _sut = new EntranceShuffleRequirementDictionary(Factory);
        return;

        EntranceShuffleRequirement Factory(EntranceShuffle expectedValue)
        {
            return new EntranceShuffleRequirement(_mode, expectedValue);
        }
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[EntranceShuffle.None];
        var requirement2 = _sut[EntranceShuffle.None];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[EntranceShuffle.None];
        var requirement2 = _sut[EntranceShuffle.Dungeon];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IEntranceShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as EntranceShuffleRequirementDictionary);
    }
}