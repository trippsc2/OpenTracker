using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Alternative;

[ExcludeFromCodeCoverage]
public sealed class AlternativeRequirementTests
{
    private readonly List<MockAccessibilityRequirement> _requirements = new()
    {
        new MockAccessibilityRequirement(),
        new MockAccessibilityRequirement()
    };

    private readonly AlternativeRequirement _sut;

    public AlternativeRequirementTests()
    {
        _sut = new AlternativeRequirement(_requirements);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
        _requirements[0].Accessibility = accessibility;
        _requirements[1].Accessibility = accessibility;

        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }
        
    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldEqualExpected(
        AccessibilityLevel expected, AccessibilityLevel requirement1, AccessibilityLevel requirement2)
    {
        _requirements[0].Accessibility = requirement1;
        _requirements[1].Accessibility = requirement2;

        _sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
        _requirements[0].Accessibility = accessibility;
        _requirements[1].Accessibility = accessibility;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();
        
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
        _requirements[0].Accessibility = accessibility;
        _requirements[1].Accessibility = accessibility;

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(false, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    [InlineData(true, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
    [InlineData(true, AccessibilityLevel.Normal, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Met_ShouldEqualExpected(
        bool expected, AccessibilityLevel requirement1, AccessibilityLevel requirement2)
    {
        _requirements[0].Accessibility = requirement1;
        _requirements[1].Accessibility = requirement2;

        _sut.Met.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<AlternativeRequirement.Factory>();
        var sut1 = factory(_requirements);
        var sut2 = factory(_requirements);

        sut1.Should().NotBeSameAs(sut2);
    }
}