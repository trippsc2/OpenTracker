using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.AlwaysDisplayDungeonItems;

public class AlwaysDisplayDungeonItemsRequirementDictionaryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly AlwaysDisplayDungeonItemsRequirementDictionary _sut;

    public AlwaysDisplayDungeonItemsRequirementDictionaryTests()
    {
        static IAlwaysDisplayDungeonItemsRequirement Factory(bool expectedValue)
        {
            return Substitute.For<IAlwaysDisplayDungeonItemsRequirement>();
        }

        _sut = new AlwaysDisplayDungeonItemsRequirementDictionary(Factory);
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