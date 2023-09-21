using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.BigKeyShuffle;

[ExcludeFromCodeCoverage]
public sealed class BigKeyShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly BigKeyShuffleRequirementDictionary _sut;

    public BigKeyShuffleRequirementDictionaryTests()
    {
        _sut = new BigKeyShuffleRequirementDictionary(Factory);
        return;

        BigKeyShuffleRequirement Factory(bool expectedValue)
        {
            return new BigKeyShuffleRequirement(_mode, expectedValue);
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
        var sut = scope.Resolve<IBigKeyShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as BigKeyShuffleRequirementDictionary);
    }
}