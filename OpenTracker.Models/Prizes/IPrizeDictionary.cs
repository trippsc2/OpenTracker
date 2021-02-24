using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the interface for the dictionary of prizes.
    /// </summary>
    public interface IPrizeDictionary : IDictionary<PrizeType, IItem>,
        ICollection<KeyValuePair<PrizeType, IItem>>
    {
    }
}