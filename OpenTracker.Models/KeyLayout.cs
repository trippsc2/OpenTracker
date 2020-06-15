using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class KeyLayout
    {
        public List<DungeonItemID> BigKeyLocations { get; }
        public List<DungeonItemID> SmallKeyLocations { get; }
        public int SmallKeyCount { get; }
        public ModeRequirement ModeRequirement { get; }

        public KeyLayout(List<DungeonItemID> smallKeyLocations,
            int smallKeyCount, List<DungeonItemID> bigKeyLocations,
            ModeRequirement modeRequirement)
        {
            BigKeyLocations = bigKeyLocations;
            SmallKeyLocations = smallKeyLocations;
            SmallKeyCount = smallKeyCount;
            ModeRequirement = modeRequirement;
        }
    }
}
