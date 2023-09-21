using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.SmallKeyShuffle;

[ExcludeFromCodeCoverage]
public sealed class SmallKeyShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly SmallKeyShuffleRequirementDictionary _sut;

    public SmallKeyShuffleRequirementDictionaryTests()
    {
        _sut = new SmallKeyShuffleRequirementDictionary(Factory);
        return;

        SmallKeyShuffleRequirement Factory(bool expectedValue)
        {
            return new SmallKeyShuffleRequirement(_mode, expectedValue);
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
        var sut = scope.Resolve<ISmallKeyShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as SmallKeyShuffleRequirementDictionary);
    }
}