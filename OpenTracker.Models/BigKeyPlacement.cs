using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class BigKeyPlacement
    {
        public List<DungeonItemID> Placements { get; }
        public ModeRequirement ModeRequirement { get; }

        public BigKeyPlacement(List<DungeonItemID> placements, ModeRequirement modeRequirement)
        {
            Placements = placements;
            ModeRequirement = modeRequirement;
        }
    }
}
