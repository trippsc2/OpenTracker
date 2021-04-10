using System.Collections.Generic;
using OpenTracker.Models.Prizes;

namespace OpenTracker.Models.Requirements.Item.Prizes
{
    /// <summary>
    ///     This interface contains the dictionary container for prize requirements.
    /// </summary>
    public interface IPrizeRequirementDictionary : IDictionary<(PrizeType type, int count), IRequirement>
    {
    }
}