using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.ShopShuffle
{
    /// <summary>
    ///     This interface contains the dictionary container for shop shuffle requirements.
    /// </summary>
    public interface IShopShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}