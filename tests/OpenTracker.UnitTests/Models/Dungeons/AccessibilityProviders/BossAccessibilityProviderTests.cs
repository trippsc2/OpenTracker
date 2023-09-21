using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.AccessibilityProviders;

[ExcludeFromCodeCoverage]
public sealed class BossAccessibilityProviderTests
{
    private readonly BossAccessibilityProvider _sut = new();

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Partial, AccessibilityLevel.Partial)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Cleared)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, AccessibilityLevel set)
    {
        _sut.Accessibility = set;

        _sut.Accessibility.Should().Be(expected);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        using var monitor = _sut.Monitor();
        
        _sut.Accessibility = AccessibilityLevel.Normal;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessibility);
    }
}