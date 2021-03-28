using System.Collections.Generic;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This class contains dungeon accessibility result data.
    /// </summary>
    public class DungeonResult : IDungeonResult
    {
        public List<AccessibilityLevel> BossAccessibility { get; }
        public int Accessible { get; }
        public bool SequenceBreak { get; }
        public bool Visible { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossAccessibility">
        /// A list of accessibility of each boss.
        /// </param>
        /// <param name="accessible">
        /// The number of accessible items.
        /// </param>
        /// <param name="sequenceBreak">
        /// A boolean representing whether the result required a sequence break or non-guaranteed item placement.
        /// </param>
        /// <param name="visible">
        /// A boolean representing whether one inaccessible item is visible.
        /// </param>
        public DungeonResult(
            List<AccessibilityLevel> bossAccessibility, int accessible, bool sequenceBreak, bool visible)
        {
            BossAccessibility = bossAccessibility;
            Accessible = accessible;
            Visible = visible;
            SequenceBreak = sequenceBreak;
        }
    }
}
