using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
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
        
    [Fact]
    public void SequenceBreakChanged_ShouldUpdateValue()
    {
        _sequenceBreak.Enabled.Returns(true);

        _sequenceBreak.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _sequenceBreak, new PropertyChangedEventArgs(nameof(ISequenceBreak.Enabled)));
            
        Assert.True(_sut.Met);
    }

    [Fact]
    public void Met_ShouldRaisePropertyChanged()
    {
        _sequenceBreak.Enabled.Returns(true);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Met), 
            () => _sequenceBreak.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sequenceBreak, new PropertyChangedEventArgs(nameof(ISequenceBreak.Enabled))));
    }

    [Fact]
    public void Met_ShouldRaiseChangePropagated()
    {
        _sequenceBreak.Enabled.Returns(true);

        var eventRaised = false;

        void Handler(object? sender, EventArgs e)
        {
            eventRaised = true;
        }
            
        _sut.ChangePropagated += Handler;
        _sequenceBreak.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _sequenceBreak, new PropertyChangedEventArgs(nameof(ISequenceBreak.Enabled)));
        _sut.ChangePropagated -= Handler;
            
        Assert.True(eventRaised);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Met_ShouldMatchExpected(bool expected, bool enabled)
    {
        _sequenceBreak.Enabled.Returns(enabled);
            
        _sequenceBreak.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _sequenceBreak, new PropertyChangedEventArgs(nameof(ISequenceBreak.Enabled)));
            
        Assert.Equal(expected, _sut.Met);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _sequenceBreak.Enabled.Returns(true);

        Assert.PropertyChanged(_sut, nameof(IRequirement.Accessibility), 
            () => _sequenceBreak.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _sequenceBreak, new PropertyChangedEventArgs(nameof(ISequenceBreak.Enabled))));
    }

    [Theory]
    [InlineData(AccessibilityLevel.None, false)]
    [InlineData(AccessibilityLevel.SequenceBreak, true)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, bool enabled)
    {
        _sequenceBreak.Enabled.Returns(enabled);
            
        _sequenceBreak.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _sequenceBreak, new PropertyChangedEventArgs(nameof(ISequenceBreak.Enabled)));
            
        Assert.Equal(expected, _sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ISequenceBreakRequirement.Factory>();
        var sut = factory(_sequenceBreak);
            
        Assert.NotNull(sut as SequenceBreakRequirement);
    }
}