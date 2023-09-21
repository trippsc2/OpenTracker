using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prize;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Item.Prize;

[ExcludeFromCodeCoverage]
public sealed class PrizeRequirementDictionaryTests
{
    private readonly IPrizeDictionary _prizes = Substitute.For<IPrizeDictionary>();

    private readonly PrizeRequirementDictionary _sut;

    public PrizeRequirementDictionaryTests()
    {
        _sut = new PrizeRequirementDictionary(_prizes, (item, count) => new ItemRequirement(item, count));
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[(PrizeType.Crystal, 1)];
        var requirement2 = _sut[(PrizeType.Crystal, 1)];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[(PrizeType.Crystal, 1)];
        var requirement2 = _sut[(PrizeType.Crystal, 2)];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IPrizeRequirementDictionary>();
            
        Assert.NotNull(sut as PrizeRequirementDictionary);
    }
}