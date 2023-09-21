using System.Diagnostics.CodeAnalysis;
using Autofac;
using OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;
using OpenTracker.Models.Settings;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.AlwaysDisplayDungeonItems;

[ExcludeFromCodeCoverage]
public sealed class AlwaysDisplayDungeonItemsRequirementDictionaryTests
{
    private readonly LayoutSettings _layoutSettings = new();
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly AlwaysDisplayDungeonItemsRequirementDictionary _sut;

    public AlwaysDisplayDungeonItemsRequirementDictionaryTests()
    {
        _sut = new AlwaysDisplayDungeonItemsRequirementDictionary(Factory);
        return;

        AlwaysDisplayDungeonItemsRequirement Factory(bool expectedValue)
        {
            return new AlwaysDisplayDungeonItemsRequirement(_layoutSettings, expectedValue);
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
        var sut = scope.Resolve<IAlwaysDisplayDungeonItemsRequirementDictionary>();
            
        Assert.NotNull(sut as AlwaysDisplayDungeonItemsRequirementDictionary);
    }
}