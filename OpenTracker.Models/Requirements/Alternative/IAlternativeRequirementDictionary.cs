using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Alternative
{
    /// <summary>
    ///     This interface contains the dictionary container for sets of alternative requirements.
    /// </summary>
    public interface IAlternativeRequirementDictionary : IDictionary<HashSet<IRequirement>, IRequirement>
    {
    }
}