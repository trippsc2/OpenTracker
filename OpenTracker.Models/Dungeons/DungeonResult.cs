using OpenTracker.Models.AccessibilityLevels;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    public class DungeonResult
    {
        public List<AccessibilityLevel> BossAccessibility { get; }
        public AccessibilityLevel Accessibility { get; }
        public int Accessible { get; }

        public DungeonResult(
            List<AccessibilityLevel> bossAccessibility, AccessibilityLevel accessibility,
            int accessible)
        {
            BossAccessibility = bossAccessibility ??
                throw new ArgumentNullException(nameof(bossAccessibility));
            Accessibility = accessibility;
            Accessible = accessible;
        }
    }
}
