using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems
{
    /// <summary>
    ///     This interface contains the dictionary container for guaranteed boss items requirements.
    /// </summary>
    public interface IGuaranteedBossItemsRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}