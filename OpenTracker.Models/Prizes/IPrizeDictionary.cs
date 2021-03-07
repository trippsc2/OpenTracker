using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains the dictionary container for prize data.
    /// </summary>
    public interface IPrizeDictionary : IDictionary<PrizeType, IItem>,
        ICollection<KeyValuePair<PrizeType, IItem>>
    {
    }
}