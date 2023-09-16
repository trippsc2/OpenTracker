using System;
using OpenTracker.Models.Accessibility;
using Xunit;

namespace OpenTracker.UnitTests.Models.Accessibility;

public class AccessibilityLevelTests
{
    [Fact]
    public void Min_ShouldThrowException_WhenProvidingNoValues()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => AccessibilityLevelMethods.Min());
    }
        
    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Cleared, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Cleared)]
    [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared)]
    public void Min_ShouldReturnTheLeastValue_WhenProvidingTwoValues(
        AccessibilityLevel expected, AccessibilityLevel a1, AccessibilityLevel a2)
    {
        Assert.Equal(expected, AccessibilityLevelMethods.Min(a1, a2));
    }

    [Theory]
    [InlineData(
        AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal,
        AccessibilityLevel.Normal)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak,
        AccessibilityLevel.Normal)]
    [InlineData(
        AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.Normal,
        AccessibilityLevel.SequenceBreak)]
    [InlineData(
        AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Cleared,
        AccessibilityLevel.Cleared)]
    [InlineData(
        AccessibilityLevel.Normal, AccessibilityLevel.Cleared, AccessibilityLevel.Normal,
        AccessibilityLevel.Cleared)]
    [InlineData(
        AccessibilityLevel.Normal, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
        AccessibilityLevel.Normal)]
    [InlineData(
        AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
        AccessibilityLevel.Cleared)]
    public void Min_ShouldReturnTheLeastValue_WhenProvidingThreeValues(
        AccessibilityLevel expected, AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel a3)
    {
        Assert.Equal(expected, AccessibilityLevelMethods.Min(a1, a2, a3));
    }

    [Fact]
    public void Max_ShouldThrowException_WhenProvidingNoValues()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => AccessibilityLevelMethods.Max());
    }
        
    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Normal, AccessibilityLevel.Cleared)]
    [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared)]
    public void Max_ShouldReturnTheGreatestValue_WhenProvidingTwoValues(
        AccessibilityLevel expected, AccessibilityLevel a1, AccessibilityLevel a2)
    {
        Assert.Equal(expected, AccessibilityLevelMethods.Max(a1, a2));
    }

    [Theory]
    [InlineData(
        AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.Inspect, AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
    [InlineData(
        AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
    [InlineData(
        AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal,
        AccessibilityLevel.Normal)]
    [InlineData(
        AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak,
        AccessibilityLevel.Normal)]
    [InlineData(
        AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal,
        AccessibilityLevel.SequenceBreak)]
    [InlineData(
        AccessibilityLevel.Cleared, AccessibilityLevel.Normal, AccessibilityLevel.Cleared,
        AccessibilityLevel.Cleared)]
    [InlineData(
        AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Normal,
        AccessibilityLevel.Cleared)]
    [InlineData(
        AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
        AccessibilityLevel.Normal)]
    [InlineData(
        AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
        AccessibilityLevel.Cleared)]
    public void Max_ShouldReturnTheGreatestValue_WhenProvidingThreeValues(
        AccessibilityLevel expected, AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel a3)
    {
        Assert.Equal(expected, AccessibilityLevelMethods.Max(a1, a2, a3));
    }
}