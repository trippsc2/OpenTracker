using System.Collections.Generic;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item.Exact
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IItemExactRequirement"/> objects indexed by <see cref="ItemType"/> and count.
    /// </summary>
    public interface IItemExactRequirementDictionary : IDictionary<(ItemType type, int count), IRequirement>
    {
    }
}