using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements.Item.Exact;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Item.Exact;

[ExcludeFromCodeCoverage]
public sealed class ItemExactRequirementDictionaryTests
{
    private readonly IItemDictionary _items = Substitute.For<IItemDictionary>();

    private readonly ItemExactRequirementDictionary _sut;

    public ItemExactRequirementDictionaryTests()
    {
        _sut = new ItemExactRequirementDictionary(
            _items, (item, count) => new ItemExactRequirement(item, count));
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[(ItemType.Sword, 1)];
        var requirement2 = _sut[(ItemType.Sword, 1)];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[(ItemType.Sword, 1)];
        var requirement2 = _sut[(ItemType.Sword, 2)];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IItemExactRequirementDictionary>();
            
        Assert.NotNull(sut as ItemExactRequirementDictionary);
    }
}