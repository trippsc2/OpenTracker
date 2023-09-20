using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.TakeAnyLocations;

[ExcludeFromCodeCoverage]
public sealed class TakeAnyLocationsRequirementDictionaryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly TakeAnyLocationsRequirementDictionary _sut;

    public TakeAnyLocationsRequirementDictionaryTests()
    {
        static ITakeAnyLocationsRequirement Factory(bool expectedValue)
        {
            return Substitute.For<ITakeAnyLocationsRequirement>();
        }

        _sut = new TakeAnyLocationsRequirementDictionary(Factory);
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[false];
        var requirement2 = _sut[false];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnDifferentInstances()
    {
        var requirement1 = _sut[false];
        var requirement2 = _sut[true];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<ITakeAnyLocationsRequirementDictionary>();
            
        Assert.NotNull(sut as TakeAnyLocationsRequirementDictionary);
    }
}