using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class BigKeyPlacement
    {
        public List<DungeonItemID> Placements { get; }
        public Mode RequiredMode { get; }

        public BigKeyPlacement(List<DungeonItemID> placements, Mode requiredMode)
        {
            Placements = placements;
            RequiredMode = requiredMode;
        }
    }
}
