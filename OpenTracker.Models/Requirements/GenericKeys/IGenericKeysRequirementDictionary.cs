using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.GenericKeys
{
    /// <summary>
    ///     This interface contains the dictionary container for generic keys requirements.
    /// </summary>
    public interface IGenericKeysRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}