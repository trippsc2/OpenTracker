using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode;

[ExcludeFromCodeCoverage]
public sealed class ItemPlacementRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly ItemPlacementRequirementDictionary _sut;

    public ItemPlacementRequirementDictionaryTests()
    {
        ItemPlacementRequirement Factory(ItemPlacement expectedValue)
        {
            return new ItemPlacementRequirement(_mode, expectedValue);
        }

        _sut = new ItemPlacementRequirementDictionary(Factory);
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[ItemPlacement.Basic];
        var requirement2 = _sut[ItemPlacement.Basic];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[ItemPlacement.Basic];
        var requirement2 = _sut[ItemPlacement.Advanced];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IItemPlacementRequirementDictionary>();
            
        Assert.NotNull(sut as ItemPlacementRequirementDictionary);
    }
}