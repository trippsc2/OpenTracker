using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    ///     This interface contains the big key layout data.
    /// </summary>
    public interface IBigKeyLayout : IKeyLayout
    {
        /// <summary>
        ///     A factory for creating new big key layouts.
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
        /// <returns>
        ///     A new big key layout.
        /// </returns>
        delegate IBigKeyLayout Factory(
            IList<DungeonItemID> bigKeyLocations, IList<IKeyLayout> children, IRequirement? requirement = null);
    }
}