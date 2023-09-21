using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements.Static;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Static;

[ExcludeFromCodeCoverage]
public sealed class StaticRequirementTests
{
    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldAlwaysReturnExpected(AccessibilityLevel expected, AccessibilityLevel requirement)
    {
        var sut = new StaticRequirement(requirement);

        sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void Met_ShouldAlwaysReturnTrue()
    {
        var sut = new StaticRequirement(AccessibilityLevel.None);
            
        sut.Met.Should().BeTrue();
    }
        
    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<StaticRequirement.Factory>();
        var sut1 = factory(AccessibilityLevel.None);
        var sut2 = factory(AccessibilityLevel.None);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}