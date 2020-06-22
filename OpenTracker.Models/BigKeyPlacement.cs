using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class BigKeyPlacement
    {
        public List<DungeonItemID> Placements { get; }
        public ModeRequirement ModeRequirement { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="placements">
        /// A list of dungeon item identifiers that can hold a big key.
        /// </param>
        /// <param name="modeRequirement">
        /// The applicable game mode requirement for this placement restriction.
        /// </param>
        public BigKeyPlacement(List<DungeonItemID> placements, ModeRequirement modeRequirement)
        {
            Placements = placements;
            ModeRequirement = modeRequirement;
        }
    }
}
