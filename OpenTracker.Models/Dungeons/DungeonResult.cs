using OpenTracker.Models.AccessibilityLevels;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This class contains dungeon accessibility result data.
    /// </summary>
    public class DungeonResult : IDungeonResult
    {
        public List<AccessibilityLevel> BossAccessibility { get; }
        public AccessibilityLevel Accessibility { get; }
        public int Accessible { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossAccessibility">
        /// A list of accessibility of each boss.
        /// </param>
        /// <param name="accessibility">
        /// The accessibility level of the dungeon items.
        /// </param>
        /// <param name="accessible">
        /// The number of accessible items.
        /// </param>
        public DungeonResult(
            List<AccessibilityLevel> bossAccessibility, AccessibilityLevel accessibility,
            int accessible)
        {
            BossAccessibility = bossAccessibility;
            Accessibility = accessibility;
            Accessible = accessible;
        }
    }
}
