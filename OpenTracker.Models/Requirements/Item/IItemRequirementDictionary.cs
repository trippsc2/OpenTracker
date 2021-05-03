using System.Collections.Generic;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    ///     This interface contains the dictionary container for item requirements.
    /// </summary>
    public interface IItemRequirementDictionary : IDictionary<(ItemType type, int count), IRequirement>
    {
    }
}