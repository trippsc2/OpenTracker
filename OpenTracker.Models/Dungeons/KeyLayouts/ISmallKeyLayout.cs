using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    ///     This interface contains small key layout data.
    /// </summary>
    public interface ISmallKeyLayout : IKeyLayout
    {
        /// <summary>
        ///     A factory for creating new small key layouts.
        /// </summary>
        /// <param name="count">
        ///     A 32-bit signed integer representing the number of keys that must be contained in the
        ///         list of locations.
        /// </param>
        /// <param name="smallKeyLocations">
        ///     The list of dungeon item IDs that the number of small keys must be contained in.
        /// </param>
        /// <param name="bigKeyInLocations">
        ///     A boolean representing whether the big key is contained in the list of locations.
        /// </param>
        /// <param name="children">
        ///     The list of child key layouts, if this layout is possible.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon parent class.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for this key layout to be valid.
        /// </param>
        /// <returns>
        ///     A new small key layout.
        /// </returns>
        delegate ISmallKeyLayout Factory(
            int count, IList<DungeonItemID> smallKeyLocations, bool bigKeyInLocations, IList<IKeyLayout> children,
            IDungeon dungeon, IRequirement requirement = null);
    }
}