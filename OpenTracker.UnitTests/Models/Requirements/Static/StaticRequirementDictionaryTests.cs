using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements.Static;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Static;

[ExcludeFromCodeCoverage]
public sealed class StaticRequirementDictionaryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly StaticRequirementDictionary _sut;

    public StaticRequirementDictionaryTests()
    {
        static IStaticRequirement Factory(AccessibilityLevel expectedValue)
        {
            return Substitute.For<IStaticRequirement>();
        }

        _sut = new StaticRequirementDictionary(Factory);
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        const AccessibilityLevel accessibility = AccessibilityLevel.None;
        var requirement1 = _sut[accessibility];
        var requirement2 = _sut[accessibility];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnDifferentInstances()
    {
        var requirement1 = _sut[AccessibilityLevel.None];
        var requirement2 = _sut[AccessibilityLevel.SequenceBreak];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IStaticRequirementDictionary>();
            
        Assert.NotNull(sut as StaticRequirementDictionary);
    }
}