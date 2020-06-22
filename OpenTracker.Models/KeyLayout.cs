using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for key layout rules of the dungeon.
    /// </summary>
    public class KeyLayout
    {
        public List<DungeonItemID> BigKeyLocations { get; }
        public List<DungeonItemID> SmallKeyLocations { get; }
        public int SmallKeyCount { get; }
        public ModeRequirement ModeRequirement { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="smallKeyLocations">
        /// A list of dungeon item IDs representing the item locations that must contain 
        /// the specified number of small keys.
        /// </param>
        /// <param name="smallKeyCount">
        /// A 32-bit integer representing the number of small keys that must be in the 
        /// specified dungeon item locations.
        /// </param>
        /// <param name="bigKeyLocations">
        /// A list of dungeon item IDs representing the required big key placement for
        /// this rule.
        /// </param>
        /// <param name="modeRequirement">
        /// The mode requirement of this key layout rule.
        /// </param>
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
