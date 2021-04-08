using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    ///     This class contains the end of key layout data.
    /// </summary>
    public class EndKeyLayout : IEndKeyLayout
    {
        private readonly IRequirement _requirement;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="requirement">
        ///     The requirement for this key layout to be valid.
        /// </param>
        public EndKeyLayout(IRequirement requirement)
        {
            _requirement = requirement;
        }

        /// <summary>
        /// Returns whether the key layout is possible in the current game state.
        /// </summary>
        /// <param name="inaccessible"></param>
        /// <param name="accessible"></param>
        /// <param name="state">
        ///     The dungeon state data.
        /// </param>
        /// <returns>
        /// A boolean representing whether the key layout is possible.
        /// </returns>
        public bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state)
        {
            return _requirement.Met;
        }
    }
}
