using System.Collections.Generic;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for the
    /// <see cref="IItemPlacementRequirement"/> objects indexed by <see cref="ItemPlacement"/>.
    /// </summary>
    public interface IItemPlacementRequirementDictionary : IDictionary<ItemPlacement, IRequirement>
    {
    }
}