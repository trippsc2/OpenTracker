using System.Collections.Generic;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.Prizes
{
    /// <summary>
    /// This interface contains the dictionary container for prize data.
    /// </summary>
    public interface IPrizeDictionary : IDictionary<PrizeType, IItem>
    {
    }
}