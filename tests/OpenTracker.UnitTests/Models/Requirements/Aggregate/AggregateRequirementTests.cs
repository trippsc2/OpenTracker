using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Autofac;
using FluentAssertions;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Aggregate;

[ExcludeFromCodeCoverage]
public sealed class AggregateRequirementTests
{
    private readonly List<MockAccessibilityRequirement> _requirements = new()
    {
        new MockAccessibilityRequirement(),
        new MockAccessibilityRequirement()
    };

    private readonly AggregateRequirement _sut;

    public AggregateRequirementTests()
    {
        _sut = new AggregateRequirement(_requirements);
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

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();
        
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
        _requirements[0].Accessibility = accessibility;
        _requirements[1].Accessibility = accessibility;
        
        Thread.Sleep(1000);

        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }
        
    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.Normal, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Normal, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    public void Accessibility_ShouldEqualExpected(
        AccessibilityLevel expected,
        AccessibilityLevel requirement1,
        AccessibilityLevel requirement2)
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
        
    [Theory]
    [InlineData(false, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(false, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(false, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
    [InlineData(false, AccessibilityLevel.None, AccessibilityLevel.Normal)]
    [InlineData(false, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.Inspect, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.Inspect, AccessibilityLevel.Normal)]
    [InlineData(false, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Inspect)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(true, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
    [InlineData(false, AccessibilityLevel.Normal, AccessibilityLevel.None)]
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
    public void AutofacResolve_ShouldResolveAsTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<AggregateRequirement.Factory>();
        var sut1 = factory(_requirements);
        var sut2 = factory(_requirements);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}