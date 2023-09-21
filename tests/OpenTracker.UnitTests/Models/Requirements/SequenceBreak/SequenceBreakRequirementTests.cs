using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.SequenceBreak;

[ExcludeFromCodeCoverage]
public sealed class SequenceBreakRequirementTests
{
    private readonly ISequenceBreak _sequenceBreak = Substitute.For<ISequenceBreak>();

    private readonly SequenceBreakRequirement _sut;

    public SequenceBreakRequirementTests()
    {
        _sut = new SequenceBreakRequirement(_sequenceBreak);
    }
    
    private void ChangeSequenceBreakEnabled(bool newValue)
    {
        _sequenceBreak.Enabled.Returns(newValue);
        _sequenceBreak.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _sequenceBreak,
                new PropertyChangedEventArgs(nameof(ISequenceBreak.Enabled)));
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeSequenceBreakEnabled(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Met);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Met_ShouldMatchExpected(bool expected, bool enabled)
    {
        ChangeSequenceBreakEnabled(enabled);
            
        _sut.Met.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        ChangeSequenceBreakEnabled(true);
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaiseChangePropagated()
    {
        using var monitor = _sut.Monitor();
        
        ChangeSequenceBreakEnabled(true);
        
        monitor.Should().Raise(nameof(IRequirement.ChangePropagated));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, false)]
    [InlineData(AccessibilityLevel.SequenceBreak, true)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, bool enabled)
    {
        ChangeSequenceBreakEnabled(enabled);

        _sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<SequenceBreakRequirement.Factory>();
        var sut1 = factory(_sequenceBreak);
        var sut2 = factory(_sequenceBreak);
            
        sut1.Should().NotBeSameAs(sut2);
    }
}