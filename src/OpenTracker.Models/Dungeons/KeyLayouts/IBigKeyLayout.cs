using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyLayouts;

/// <summary>
///     This interface contains the big key layout data.
/// </summary>
public interface IBigKeyLayout : IKeyLayout
{
    /// <summary>
    /// A factory for creating new <see cref="IBigKeyLayout"/> objects.
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
    /// <returns>
    ///     A new <see cref="IBigKeyLayout"/> object.
    /// </returns>
    delegate IBigKeyLayout Factory(
        IList<DungeonItemID> bigKeyLocations, IList<IKeyLayout> children, IRequirement? requirement = null);
}