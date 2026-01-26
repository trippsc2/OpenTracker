using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements.Item;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Item;

public class ItemRequirementDictionaryTests
{
    private readonly IItemDictionary _items = Substitute.For<IItemDictionary>();

    private readonly ItemRequirementDictionary _sut;

    public ItemRequirementDictionaryTests()
    {
        _sut = new ItemRequirementDictionary(_items, (item, count) => new ItemRequirement(item, count));
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
        var sut = scope.Resolve<IItemRequirementDictionary>();
            
        Assert.NotNull(sut as ItemRequirementDictionary);
    }
}