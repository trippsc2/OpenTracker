using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.ShopShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.ShopShuffle;

[ExcludeFromCodeCoverage]
public sealed class ShopShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly ShopShuffleRequirementDictionary _sut;

    public ShopShuffleRequirementDictionaryTests()
    {
        _sut = new ShopShuffleRequirementDictionary(Factory);
        return;

        ShopShuffleRequirement Factory(bool expectedValue)
        {
            return new ShopShuffleRequirement(_mode, expectedValue);
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
        var sut = scope.Resolve<IShopShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as ShopShuffleRequirementDictionary);
    }
}