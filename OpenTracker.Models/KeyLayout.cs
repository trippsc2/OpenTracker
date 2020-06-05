using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class KeyLayout
    {
        public List<DungeonItemID> BigKeyLocations { get; }
        public List<DungeonItemID> SmallKeyLocations { get; }
        public int SmallKeyCount { get; }
        public Mode RequiredMode { get; }

        public KeyLayout(List<DungeonItemID> smallKeyLocations,
            int smallKeyCount, List<DungeonItemID> bigKeyLocations, Mode requiredMode)
        {
            BigKeyLocations = bigKeyLocations;
            SmallKeyLocations = smallKeyLocations;
            SmallKeyCount = smallKeyCount;
            RequiredMode = requiredMode;
        }
    }
}
