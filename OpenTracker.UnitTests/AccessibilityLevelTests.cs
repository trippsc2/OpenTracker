using OpenTracker.Models.AccessibilityLevels;
using Xunit;

namespace OpenTracker.UnitTests
{
    [Collection("Tests")]
    public class AccessibilityLevelTests
    {
        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Cleared, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared)]
        public void Min_TwoValueTests(
            AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel expected)
        {
            AccessibilityLevel actual = AccessibilityLevelMethods.Min(a1, a2);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(
            AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(
            AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(
            AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(
            AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.Normal,
            AccessibilityLevel.SequenceBreak)]
        [InlineData(
            AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal,
            AccessibilityLevel.SequenceBreak)]
        [InlineData(
            AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak,
            AccessibilityLevel.SequenceBreak)]
        [InlineData(
            AccessibilityLevel.Normal, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
            AccessibilityLevel.Normal)]
        [InlineData(
            AccessibilityLevel.Cleared, AccessibilityLevel.Normal, AccessibilityLevel.Cleared,
            AccessibilityLevel.Normal)]
        [InlineData(
            AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Normal,
            AccessibilityLevel.Normal)]
        [InlineData(
            AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
            AccessibilityLevel.Cleared)]
        public void Min_ThreeValueTests(
            AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel a3,
            AccessibilityLevel expected)
        {
            AccessibilityLevel actual = AccessibilityLevelMethods.Min(a1, a2, a3);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Normal, AccessibilityLevel.Cleared)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared)]
        [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared)]
        public void Max_TwoValueTests(
            AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel expected)
        {
            AccessibilityLevel actual = AccessibilityLevelMethods.Max(a1, a2);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(
            AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(
            AccessibilityLevel.Inspect, AccessibilityLevel.None, AccessibilityLevel.None,
            AccessibilityLevel.Inspect)]
        [InlineData(
            AccessibilityLevel.None, AccessibilityLevel.Inspect, AccessibilityLevel.None,
            AccessibilityLevel.Inspect)]
        [InlineData(
            AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.Inspect,
            AccessibilityLevel.Inspect)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.Normal,
            AccessibilityLevel.Normal)]
        [InlineData(
            AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal,
            AccessibilityLevel.Normal)]
        [InlineData(
            AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak,
            AccessibilityLevel.Normal)]
        [InlineData(
            AccessibilityLevel.Normal, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
            AccessibilityLevel.Cleared)]
        [InlineData(
            AccessibilityLevel.Cleared, AccessibilityLevel.Normal, AccessibilityLevel.Cleared,
            AccessibilityLevel.Cleared)]
        [InlineData(
            AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Normal,
            AccessibilityLevel.Cleared)]
        [InlineData(
            AccessibilityLevel.Cleared, AccessibilityLevel.Cleared, AccessibilityLevel.Cleared,
            AccessibilityLevel.Cleared)]
        public void Max_ThreeValueTests(
            AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel a3,
            AccessibilityLevel expected)
        {
            AccessibilityLevel actual = AccessibilityLevelMethods.Max(a1, a2, a3);
            Assert.Equal(expected, actual);
        }
    }
}
