using System.Collections.Generic;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item.Exact
{
    /// <summary>
    ///     This interface contains the dictionary container for item exact requirements.
    /// </summary>
    public interface IItemExactRequirementDictionary : IDictionary<(ItemType type, int count), IRequirement>
    {
    }
}