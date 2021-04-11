using System.Collections.Generic;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    ///     This interface contains the dictionary container for requirement data.
    /// </summary>
    public interface IRequirementDictionary : IDictionary<RequirementType, IRequirement>
    {
    }
}