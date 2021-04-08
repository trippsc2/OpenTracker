using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    ///     This class contains the big key layout data.
    /// </summary>
    public class BigKeyLayout : IBigKeyLayout
    {
        private readonly IList<DungeonItemID> _bigKeyLocations;
        private readonly IList<IKeyLayout> _children;
        private readonly IRequirement _requirement;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="bigKeyLocations">
        ///     The list of dungeon item IDs that can contain the big key.
        /// </param>
        /// <param name="children">
        ///     The list of child key layouts, if this layout is possible.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this key layout to be valid.
        /// </param>
        public BigKeyLayout(IList<DungeonItemID> bigKeyLocations, IList<IKeyLayout> children, IRequirement requirement)
        {
            _bigKeyLocations = bigKeyLocations;
            _children = children;
            _requirement = requirement;
        }

        public bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state)
        {
            if (!_requirement.Met)
            {
                return false;
            }

            switch (state.BigKeyCollected)
            {
                case true when !_bigKeyLocations.Any(accessible.Contains):
                case false when !_bigKeyLocations.Any(inaccessible.Contains):
                    return false;
            }

            return _children.Any(child => child.CanBeTrue(inaccessible, accessible, state));
        }
    }
}
