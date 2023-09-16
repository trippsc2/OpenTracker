using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.BigKeyShuffle;

public class BigKeyShuffleRequirementDictionaryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly BigKeyShuffleRequirementDictionary _sut;

    public BigKeyShuffleRequirementDictionaryTests()
    {
        static IBigKeyShuffleRequirement Factory(bool expectedValue)
        {
            return Substitute.For<IBigKeyShuffleRequirement>();
        }

        _sut = new BigKeyShuffleRequirementDictionary(Factory);
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