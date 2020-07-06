using OpenTracker.Models.Enums;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class BigKeyPlacement
    {
        public List<DungeonItemID> Placements { get; }
        public IRequirement Requirement { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="placements">
        /// A list of dungeon item identifiers that can hold a big key.
        /// </param>
        /// <param name="requirement">
        /// The applicable game mode requirement for this placement restriction.
        /// </param>
        public BigKeyPlacement(List<DungeonItemID> placements, IRequirement requirement = null)
        {
            Placements = placements;

            if (requirement != null)
            {
                Requirement = requirement;
            }
            else
            {
                Requirement = RequirementDictionary.Instance[RequirementType.None];
            }
        }
    }
}
