using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.KeyDropShuffle;

[ExcludeFromCodeCoverage]
public sealed class KeyDropShuffleRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly KeyDropShuffleRequirementDictionary _sut;

    public KeyDropShuffleRequirementDictionaryTests()
    {
        _sut = new KeyDropShuffleRequirementDictionary(Factory);
        return;

        KeyDropShuffleRequirement Factory(bool expectedValue)
        {
            return new KeyDropShuffleRequirement(_mode, expectedValue);
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
        var sut = scope.Resolve<IKeyDropShuffleRequirementDictionary>();
            
        Assert.NotNull(sut as KeyDropShuffleRequirementDictionary);
    }
}