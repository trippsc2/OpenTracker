using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.BossShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.BossShuffle;

[ExcludeFromCodeCoverage]
public sealed class BossShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly BossShuffleRequirementDictionary _sut;

    public BossShuffleRequirementDictionaryTests()
    {
        _sut = new BossShuffleRequirementDictionary(Factory);
        return;

        BossShuffleRequirement Factory(bool expectedValue)
        {
            return new BossShuffleRequirement(_mode, expectedValue);
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
        var sut = scope.Resolve<IBossShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as BossShuffleRequirementDictionary);
    }
}