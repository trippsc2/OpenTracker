using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.KeyLayouts;

/// <summary>
/// This class contains small key layout data.
/// </summary>
[DependencyInjection]
public sealed class SmallKeyLayout : ISmallKeyLayout
{
    private readonly int _count;
    private readonly IList<DungeonItemID> _smallKeyLocations;
    private readonly bool _bigKeyInLocations;
    private readonly IList<IKeyLayout> _children;
    private readonly IRequirement? _requirement;
    private readonly IDungeon _dungeon;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="count">
    ///     A <see cref="int"/> representing the number of keys that must be contained in the locations.
    /// </param>
    /// <param name="smallKeyLocations">
    ///     The <see cref="IList{T}"/> of <see cref="DungeonItemID"/> in which the small keys must be.
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
    public SmallKeyLayout(
        int count, IList<DungeonItemID> smallKeyLocations, bool bigKeyInLocations, IList<IKeyLayout> children,
        IDungeon dungeon, IRequirement? requirement = null)
    {
        _count = count;
        _smallKeyLocations = smallKeyLocations;
        _bigKeyInLocations = bigKeyInLocations;
        _children = children;
        _dungeon = dungeon;
        _requirement = requirement;
    }

    public bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state)
    {
        if (_requirement is not null && !_requirement.Met)
        {
            return false;
        }

        var inaccessibleCount = _smallKeyLocations.Count(inaccessible.Contains);
        var inaccessibleWithoutBigKey = inaccessibleCount;
            
        if (_bigKeyInLocations && !state.BigKeyCollected)
        {
            inaccessibleWithoutBigKey--;
        }

        return ValidateMinimumKeyCount(state.KeysCollected, inaccessibleWithoutBigKey) &&
               ValidateMaximumKeyCount(state.KeysCollected, state.BigKeyCollected, inaccessibleCount) &&
               _children.Any(child => child.CanBeTrue(inaccessible, accessible, state));
    }

    /// <summary>
    /// Returns whether the minimum number of keys have been collected for the key layout to be valid.
    /// </summary>
    /// <param name="collectedKeys">
    ///     A <see cref="int"/> representing the number of small keys collected.
    /// </param>
    /// <param name="inaccessible">
    ///     A <see cref="int"/> representing the number of inaccessible locations.
    /// </param>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the key layout is possible.
    /// </returns>
    private bool ValidateMinimumKeyCount(int collectedKeys, int inaccessible)
    {
        return collectedKeys >= Math.Max(0, _count - inaccessible);
    }

    /// <summary>
    /// Returns whether the more than maximum number of keys have not been collected for the key layout to be valid.
    /// </summary>
    /// <param name="collectedKeys">
    ///     A <see cref="int"/> representing the number of small keys collected.
    /// </param>
    /// <param name="bigKeyCollected">
    ///     A <see cref="bool"/> representing whether the big key has been collected.
    /// </param>
    /// <param name="inaccessible">
    ///     A <see cref="int"/> representing the number of inaccessible locations.
    /// </param>
    /// <returns>
    ///     A <see cref="bool"/> representing whether the key layout is possible.
    /// </returns>
    private bool ValidateMaximumKeyCount(int collectedKeys, bool bigKeyCollected, int inaccessible)
    {
        var locationCount = _smallKeyLocations.Count;

        if (_bigKeyInLocations && bigKeyCollected)
        {
            locationCount--;
        }
            
        return collectedKeys <= _dungeon.SmallKey.Maximum - Math.Max(0, inaccessible - (locationCount - _count));
    }
}