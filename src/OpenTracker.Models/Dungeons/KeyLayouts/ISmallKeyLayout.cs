using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts;

/// <summary>
/// This interface contains small key layout data.
/// </summary>
public interface ISmallKeyLayout : IKeyLayout
{
    /// <summary>
    /// A factory for creating new <see cref="ISmallKeyLayout"/> objects.
    /// </summary>
    /// <param name="count">
    ///     A <see cref="int"/> representing the number of keys that must be contained in the locations.
    /// </param>
    /// <param name="smallKeyLocations">
    ///     The <see cref="IList{T}"/> of <see cref="DungeonItemID"/> in which the small keys must be contained.
    /// </param>
    /// <param name="bigKeyInLocations">
    ///     A <see cref="bool"/> representing whether the big key is contained in the locations.
    /// </param>
    /// <param name="children">
    ///     The <see cref="IList{T}"/> of child <see cref="IKeyLayout"/>, if this layout is possible.
    /// </param>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/> parent class.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for this key layout to be valid.
    /// </param>
    /// <returns>
    ///     A new <see cref="ISmallKeyLayout"/> object.
    /// </returns>
    delegate ISmallKeyLayout Factory(
        int count, IList<DungeonItemID> smallKeyLocations, bool bigKeyInLocations, IList<IKeyLayout> children,
        IDungeon dungeon, IRequirement? requirement = null);
}