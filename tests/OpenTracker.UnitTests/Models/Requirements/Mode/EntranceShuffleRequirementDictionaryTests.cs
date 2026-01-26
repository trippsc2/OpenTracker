using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

public class EntranceShuffleRequirementDictionaryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly EntranceShuffleRequirementDictionary _sut;

    public EntranceShuffleRequirementDictionaryTests()
    {
        static IEntranceShuffleRequirement Factory(EntranceShuffle expectedValue)
        {
            return Substitute.For<IEntranceShuffleRequirement>();
        }

        _sut = new EntranceShuffleRequirementDictionary(Factory);
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