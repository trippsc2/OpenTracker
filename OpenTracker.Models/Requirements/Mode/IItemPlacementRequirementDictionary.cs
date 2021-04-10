using System.Collections.Generic;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This interface contains the dictionary container for the item placement requirements.
    /// </summary>
    public interface IItemPlacementRequirementDictionary : IDictionary<ItemPlacement, IRequirement>
    {
    }
}