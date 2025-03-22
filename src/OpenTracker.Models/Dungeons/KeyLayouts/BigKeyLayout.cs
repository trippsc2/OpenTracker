using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    /// This class contains the big key layout data.
    /// </summary>
    public class BigKeyLayout : IBigKeyLayout
    {
        private readonly IList<DungeonItemID> _bigKeyLocations;
        private readonly IList<IKeyLayout> _children;
        private readonly IRequirement? _requirement;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bigKeyLocations">
        ///     The <see cref="IList{T}"/> of <see cref="DungeonItemID"/> that can contain the big key.
        /// </param>
        /// <param name="children">
        ///     The <see cref="IList{T}"/> of child <see cref="IKeyLayout"/>, if this layout is possible.
        /// </param>
        /// <param name="requirement">
        ///     The <see cref="IRequirement"/> for this key layout to be valid.
        /// </param>
        public BigKeyLayout(
            IList<DungeonItemID> bigKeyLocations, IList<IKeyLayout> children, IRequirement? requirement = null)
        {
            _bigKeyLocations = bigKeyLocations;
            _children = children;
            _requirement = requirement;
        }

        public bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state)
        {
            if (_requirement is not null && !_requirement.Met)
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
