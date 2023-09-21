using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.GuaranteedBossItems;

[ExcludeFromCodeCoverage]
public sealed class GuaranteedBossItemsRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly GuaranteedBossItemsRequirementDictionary _sut;

    public GuaranteedBossItemsRequirementDictionaryTests()
    {
        _sut = new GuaranteedBossItemsRequirementDictionary(Factory);
        return;

        GuaranteedBossItemsRequirement Factory(bool expectedValue)
        {
            return new GuaranteedBossItemsRequirement(_mode, expectedValue);
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
        var sut = scope.Resolve<IGuaranteedBossItemsRequirementDictionary>();
            
        Assert.NotNull(sut as GuaranteedBossItemsRequirementDictionary);
    }
}